using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Microsoft.Data.SqlClient;

namespace ElevatorApp
{
    /// <summary>
    /// All SQL Server access lives here. Every operation goes through Run(), so connection
    /// handling, locking, first-run database creation and error behaviour are written once.
    /// Log records use the disconnected model: a SqlDataAdapter fills a DataTable, and
    /// adapter.Update() sends the changes back (no ExecuteNonQuery for log data).
    /// The database file is kept in a "Data" folder next to the program (relative path).
    /// </summary>
    public static class LogRepository
    {
        public static readonly string[] EventTypes =
            { "All events", "Requests", "Movement", "Doors", "Safety (stop/alarm)" };

        public static readonly string[] ElevatorFilters = { "All elevators", "A", "B", "System" };

        private const string DbName = "ElevatorDb";
        private const string SelectAllSql = "SELECT Id, LoggedAt, Elevator, CarFloor, EventText FROM dbo.ElevatorLog";
        // The LocalDB instance to use. Change it here if your instance has another name
        // (run "sqllocaldb info" in a command prompt to list them), or set the environment
        // variable ELEVATOR_SQL_SERVER, e.g. (localdb)\ProjectModels
        private static readonly string Server =
            string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ELEVATOR_SQL_SERVER"))
                ? @"(localdb)\MSSQLLocalDB"
                : Environment.GetEnvironmentVariable("ELEVATOR_SQL_SERVER");

        public static string ServerName { get { return Server; } }

        private static readonly object Gate = new object();
        private static readonly string DataFolder = Path.Combine(AppContext.BaseDirectory, "Data");
        private static readonly string MdfPath = Path.Combine(DataFolder, DbName + ".mdf");
        private static bool _ready;

        // ---------------- public API ----------------

        public static void Initialize()
        {
            lock (Gate)
            {
                EnsureReady();
            }
        }

        /// <summary>Saves one event: adds a row to a DataTable and sends it with adapter.Update().</summary>
        public static void Add(DateTime timestamp, string elevator, int floor, string message)
        {
            Run(connection =>
            {
                using (var adapter = new SqlDataAdapter(SelectAllSql + " WHERE 1 = 0", connection))
                using (new SqlCommandBuilder(adapter)) // builds the INSERT command for Update()
                {
                    adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    var table = new DataTable();
                    adapter.Fill(table);               // empty table with the right columns

                    DataRow row = table.NewRow();
                    row["LoggedAt"] = timestamp;
                    row["Elevator"] = elevator;
                    row["CarFloor"] = floor;
                    row["EventText"] = message;
                    table.Rows.Add(row);

                    adapter.Update(table);             // INSERT
                }
                return 0;
            });
        }

        /// <summary>Deletes every record: marks the rows deleted in a DataTable, then adapter.Update().</summary>
        public static void Clear()
        {
            Run(connection =>
            {
                using (var adapter = new SqlDataAdapter(SelectAllSql, connection))
                using (var builder = new SqlCommandBuilder(adapter))
                {
                    adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    var table = new DataTable();
                    adapter.Fill(table);

                    foreach (DataRow row in table.Rows)
                    {
                        row.Delete();
                    }

                    builder.ConflictOption = ConflictOption.OverwriteChanges; // match on Id only
                    adapter.DeleteCommand = builder.GetDeleteCommand();
                    adapter.DeleteCommand.UpdatedRowSource = UpdateRowSource.None;
                    adapter.UpdateBatchSize = 200;     // send the deletes in batches
                    adapter.Update(table);             // DELETE
                }
                return 0;
            });
        }

        public static DataTable GetFiltered(DateTime? from, DateTime? to, string elevator, string type)
        {
            var conditions = new List<string>();
            var parameters = new List<(string Name, object Value)>();

            if (from.HasValue)
            {
                conditions.Add("LoggedAt >= @from");
                parameters.Add(("@from", from.Value.Date));
            }
            if (to.HasValue)
            {
                conditions.Add("LoggedAt < @to");
                parameters.Add(("@to", to.Value.Date.AddDays(1)));
            }
            if (!string.IsNullOrEmpty(elevator) && elevator != ElevatorFilters[0])
            {
                conditions.Add("Elevator = @el");
                parameters.Add(("@el", elevator));
            }

            string[] patterns = PatternsFor(type);
            if (patterns.Length > 0)
            {
                var parts = new List<string>();
                for (int i = 0; i < patterns.Length; i++)
                {
                    parts.Add("EventText LIKE @p" + i);
                    parameters.Add(("@p" + i, patterns[i]));
                }
                conditions.Add("(" + string.Join(" OR ", parts) + ")");
            }

            string sql = "SELECT TOP 500 Id, LoggedAt AS [Time], Elevator, CarFloor AS [Floor], EventText AS [Event] " +
                         "FROM dbo.ElevatorLog" +
                         (conditions.Count > 0 ? " WHERE " + string.Join(" AND ", conditions) : "") +
                         " ORDER BY LoggedAt DESC, Id DESC";
            return Query(sql, parameters.ToArray());
        }

        private static string[] PatternsFor(string type)
        {
            switch (type)
            {
                case "Requests": return new[] { "Request button%", "Control panel button%", "Ignored%" };
                case "Movement": return new[] { "Moving%", "Arrived%" };
                case "Doors": return new[] { "Doors%", "Door button%" };
                case "Safety (stop/alarm)": return new[] { "STOP%", "ALARM%", "Recovering%" };
                default: return new string[0];
            }
        }

        // ---------------- shared helpers (no duplicated database code) ----------------

        private static string ConnectionString(string database)
        {
            return "Server=" + Server + ";Database=" + database +
                   ";Integrated Security=true;TrustServerCertificate=true;Connect Timeout=30";
        }

        private static T RunRaw<T>(string database, Func<SqlConnection, T> work)
        {
            using (var connection = new SqlConnection(ConnectionString(database)))
            {
                connection.Open();
                return work(connection);
            }
        }

        private static T Run<T>(Func<SqlConnection, T> work)
        {
            lock (Gate)
            {
                EnsureReady();
                return RunRaw(DbName, work);
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, string sql,
                                                (string Name, object Value)[] parameters)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            foreach (var p in parameters)
            {
                command.Parameters.AddWithValue(p.Name, p.Value);
            }
            return command;
        }

        private static int ExecuteRaw(string database, string sql, params (string Name, object Value)[] parameters)
        {
            return RunRaw(database, connection =>
            {
                using (SqlCommand command = CreateCommand(connection, sql, parameters))
                {
                    return command.ExecuteNonQuery();
                }
            });
        }

        private static DataTable Query(string sql, params (string Name, object Value)[] parameters)
        {
            return Run(connection =>
            {
                var table = new DataTable();
                using (SqlCommand command = CreateCommand(connection, sql, parameters))
                using (var adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(table);               // disconnected: the grid works on this copy
                }
                return table;
            });
        }

        /// <summary>Creates (or re-attaches) Data\ElevatorDb.mdf and the log table on first use.</summary>
        private static void EnsureReady()
        {
            if (_ready)
            {
                return;
            }

            Directory.CreateDirectory(DataFolder);
            string safePath = MdfPath.Replace("'", "''");

            string registeredPath = RunRaw("master", connection =>
            {
                using (SqlCommand command = CreateCommand(connection,
                    "SELECT TOP 1 physical_name FROM sys.master_files WHERE database_id = DB_ID(@n) AND type = 0",
                    new[] { ("@n", (object)DbName) }))
                {
                    return command.ExecuteScalar() as string;
                }
            });

            // The database is registered in LocalDB, but either under another folder (the program was
            // moved) or its file is gone (the bin folder was cleaned or rebuilt). Forget the old
            // registration so the database can be attached or created here. Without this the
            // connection fails with "cannot open database".
            bool registeredHere = registeredPath != null &&
                                  string.Equals(registeredPath, MdfPath, StringComparison.OrdinalIgnoreCase);
            if (registeredPath != null && !(registeredHere && File.Exists(MdfPath)))
            {
                ForgetRegistration(File.Exists(registeredPath));
                registeredPath = null;
            }

            if (registeredPath == null)
            {
                string createSql = File.Exists(MdfPath)
                    ? "CREATE DATABASE [" + DbName + "] ON (FILENAME = N'" + safePath + "') FOR ATTACH"
                    : "CREATE DATABASE [" + DbName + "] ON PRIMARY (NAME = N'" + DbName + "', FILENAME = N'" + safePath + "')";
                ExecuteRaw("master", createSql);
            }

            ExecuteRaw(DbName,
                "IF OBJECT_ID(N'dbo.ElevatorLog', N'U') IS NULL " +
                "CREATE TABLE dbo.ElevatorLog (" +
                "Id INT IDENTITY(1,1) PRIMARY KEY, " +
                "LoggedAt DATETIME2(3) NOT NULL, " +
                "Elevator NVARCHAR(10) NOT NULL, " +
                "CarFloor INT NOT NULL, " +
                "EventText NVARCHAR(200) NOT NULL)");

            // Table created by the earlier single-elevator version: add the new column.
            ExecuteRaw(DbName,
                "IF COL_LENGTH(N'dbo.ElevatorLog', N'Elevator') IS NULL " +
                "ALTER TABLE dbo.ElevatorLog ADD Elevator NVARCHAR(10) NOT NULL " +
                "CONSTRAINT DF_ElevatorLog_Elevator DEFAULT N'A'");

            _ready = true;
        }

        /// <summary>
        /// Removes the registration of the log database from LocalDB: detaches it when its files still
        /// exist (they are re-attached from the new folder), drops it when they are gone. Other
        /// connections to it (for example SSMS) are disconnected first.
        /// </summary>
        private static void ForgetRegistration(bool filesExist)
        {
            try
            {
                ExecuteRaw("master", "ALTER DATABASE [" + DbName + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
            }
            catch (SqlException)
            {
                // e.g. the files are missing: carry on with the drop below
            }

            if (filesExist)
            {
                ExecuteRaw("master", "EXEC sp_detach_db @dbname = @n", ("@n", DbName));
            }
            else
            {
                ExecuteRaw("master", "DROP DATABASE [" + DbName + "]");
            }
        }
    }
}
