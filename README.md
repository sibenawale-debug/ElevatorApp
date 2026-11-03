# Two-Elevator Control (C# WinForms + SQL Server LocalDB)

## Run
Requires Windows, the .NET 8 SDK (or Visual Studio 2022) and SQL Server LocalDB
(installed with Visual Studio / SQL Server Express).

    dotnet run

or open `ElevatorApp.csproj` in Visual Studio and press F5. NuGet packages restore automatically.
Build once before opening the **[Design]** tab of `MainForm` (it contains two `ElevatorView` controls).

## Database
Log records use the disconnected model: `LogRepository` fills a `DataTable` with a `SqlDataAdapter` and saves or clears rows with `adapter.Update()` (no `ExecuteNonQuery()` for log data; only the one-time database creation uses it).
The server name is `(localdb)\MSSQLLocalDB`. If your LocalDB instance has another name (`sqllocaldb info` lists them),
change `Server` at the top of `LogRepository.cs` or set the environment variable `ELEVATOR_SQL_SERVER`.
On first run the program creates `Data\ElevatorDb.mdf` next to the executable (relative path) on
`(localdb)\MSSQLLocalDB` and the table `dbo.ElevatorLog (Id, LoggedAt, Elevator, CarFloor, EventText)`.
In SSMS: connect to `(localdb)\MSSQLLocalDB` -> Databases -> ElevatorDb -> Tables -> dbo.ElevatorLog.
Inside the program, press **Show database** to open the log in its own window.

## Where each task is implemented
| Task | Where |
|------|-------|
| 1 GUI | `MainForm.Designer.cs`, `ElevatorView.Designer.cs` (one elevator: shaft, 2 floor displays, own control panel), `LogForm.Designer.cs` (database window) |
| 2 Control logic | `Elevator.cs` + `ElevatorStates.cs` |
| 3 Logging and data | `LogRepository.cs` (relative path, one shared `Run` helper, exceptions handled) |
| 4 Animation | `ElevatorView.cs` (Timers) plugged into `Elevator` as delegates (`Action<int, Action>`) |
| 5.1 BackgroundWorker | `BackgroundTask.cs` - every database call runs off the GUI thread |
| 5.2 Two elevators | State pattern: `ElevatorStates.cs`. Observer pattern: `IElevatorObserver` in `ElevatorObserver.cs` (observed by the views and the main form). |

## How the two elevators work
- **Outside call buttons**: each elevator has its own pair (floor 1 and floor 2). The left column in the
  middle panel calls **A**, the right column calls **B**. A button talks only to its own elevator
  (`Elevator.HallRequest`); there is no dispatcher. The light goes off when that elevator opens its doors
  on that floor, when the request is refused, or when the elevator is stopped.
- **Each control panel** (floor 1/2, Open/Close door, STOP, ALARM) belongs to one elevator and is handled
  only by it.
- STOP freezes the car; ALARM beeps, counts down 5 s, moves the car to the nearest floor and opens the doors.
