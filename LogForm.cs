using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ElevatorApp
{
    /// <summary>The database window: shows the stored log records with filters.</summary>
    public partial class LogForm : Form
    {
        public LogForm()
        {
            InitializeComponent();
            elevatorCombo.Items.AddRange(LogRepository.ElevatorFilters);
            elevatorCombo.SelectedIndex = 0;
            typeCombo.Items.AddRange(LogRepository.EventTypes);
            typeCombo.SelectedIndex = 0;
        }

        private void LogForm_Load(object sender, EventArgs e)
        {
            RefreshLog();
        }

        /// <summary>Reloads the grid using the current filters (also called when new events arrive).</summary>
        public void RefreshLog()
        {
            DateTime? from = fromPicker.Checked ? fromPicker.Value : (DateTime?)null;
            DateTime? to = toPicker.Checked ? toPicker.Value : (DateTime?)null;
            string elevator = elevatorCombo.SelectedItem as string;
            string type = typeCombo.SelectedItem as string;

            BackgroundTask.Run<DataTable>(this,
                () => LogRepository.GetFiltered(from, to, elevator, type),
                table =>
                {
                    logGrid.DataSource = table;
                    if (logGrid.Columns.Contains("Time"))
                    {
                        logGrid.Columns["Time"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss.fff";
                    }
                    SetStatus(table.Rows.Count + " record(s) shown", false);
                },
                ex => SetStatus("Database error: " + ex.Message, true));
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            RefreshLog();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show(this, "Delete all log records from the database?",
                                                  "Clear log", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (answer != DialogResult.Yes)
            {
                return;
            }

            BackgroundTask.Run<object>(this,
                () => { LogRepository.Clear(); return null; },
                r => RefreshLog(),
                ex => SetStatus("Database error: " + ex.Message, true));
        }

        private void SetStatus(string text, bool isError)
        {
            statusLabel.ForeColor = isError ? Color.Firebrick : SystemColors.ControlText;
            statusLabel.Text = text;
        }
    }
}
