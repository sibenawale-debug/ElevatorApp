using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ElevatorApp
{
    // The layout lives in MainForm.Designer.cs. This file wires the two elevators, their own
    // outside call buttons and the database logging together.
    public partial class MainForm : Form, IElevatorObserver
    {
        private readonly Elevator _elevatorA = new Elevator("A", Elevator.BottomFloor);
        private readonly Elevator _elevatorB = new Elevator("B", Elevator.TopFloor);

        // The outside call buttons of each elevator, indexed by floor (index 0 is unused).
        private readonly Dictionary<Elevator, Button[]> _callButtons = new Dictionary<Elevator, Button[]>();

        private LogForm _logForm;
        private bool _dbErrorShown;

        public MainForm()
        {
            InitializeComponent();

            _callButtons[_elevatorA] = new Button[Elevator.TopFloor + 1];
            _callButtons[_elevatorA][1] = callButtonA1;
            _callButtons[_elevatorA][2] = callButtonA2;
            _callButtons[_elevatorB] = new Button[Elevator.TopFloor + 1];
            _callButtons[_elevatorB][1] = callButtonB1;
            _callButtons[_elevatorB][2] = callButtonB2;

            elevatorViewA.Bind(_elevatorA, "Elevator A");
            elevatorViewB.Bind(_elevatorB, "Elevator B");
            _elevatorA.Attach(this);   // this form observes both elevators (to log what they do
            _elevatorB.Attach(this);   // and to switch the call-button lights off)

            BackgroundTask.Run<object>(this,
                () => { LogRepository.Initialize(); return null; }, null, OnDatabaseError);
            LogEvent("System", Elevator.BottomFloor, "System started");
        }

        // =====================================================================
        // outside call buttons: each one belongs to exactly one elevator
        // =====================================================================

        private void callButtonA1_Click(object sender, EventArgs e) { CallElevator(_elevatorA, 1); }
        private void callButtonA2_Click(object sender, EventArgs e) { CallElevator(_elevatorA, 2); }
        private void callButtonB1_Click(object sender, EventArgs e) { CallElevator(_elevatorB, 1); }
        private void callButtonB2_Click(object sender, EventArgs e) { CallElevator(_elevatorB, 2); }

        private void CallElevator(Elevator elevator, int floor)
        {
            Button button = _callButtons[elevator][floor];
            SetLight(button, true);
            if (!elevator.HallRequest(floor))
            {
                SetLight(button, false); // refused (e.g. that elevator is stopped)
            }
        }

        private static void SetLight(Button button, bool on)
        {
            button.BackColor = on ? Color.Gold : SystemColors.Control;
        }

        // =====================================================================
        // Observer: every message an elevator reports goes to the database
        // =====================================================================

        public void Update(Elevator elevator, ElevatorEvent e)
        {
            switch (e.Kind)
            {
                case ElevatorEventKind.Message:
                    LogEvent(elevator.Name, elevator.CurrentFloor, e.Message);
                    break;

                case ElevatorEventKind.Arrived:
                    // the elevator is on that floor with open doors: its call light goes off
                    SetLight(_callButtons[elevator][e.Value], false);
                    break;

                case ElevatorEventKind.StateChanged:
                    if (e.Status == ElevatorStatus.Stopped)
                    {
                        // STOP clears the waiting requests, so the call lights go off too
                        for (int floor = Elevator.BottomFloor; floor <= Elevator.TopFloor; floor++)
                        {
                            SetLight(_callButtons[elevator][floor], false);
                        }
                    }
                    break;
            }
        }

        // =====================================================================
        // database window
        // =====================================================================

        private void showDatabaseButton_Click(object sender, EventArgs e)
        {
            if (_logForm == null || _logForm.IsDisposed)
            {
                _logForm = new LogForm();
                _logForm.FormClosed += (s, args) => _logForm = null;
                _logForm.Show(this);
            }
            else
            {
                _logForm.Activate();
            }
        }

        // =====================================================================
        // logging with SQL Server, off the GUI thread
        // =====================================================================

        private void LogEvent(string who, int floor, string message)
        {
            DateTime timestamp = DateTime.Now;     // captured here so the order is always right
            string prefix = who == "System" ? "" : "Elevator " + who + ": ";
            SetStatus(timestamp.ToString("HH:mm:ss") + "  " + prefix + message, false);

            BackgroundTask.Run<object>(this,
                () => { LogRepository.Add(timestamp, who, floor, message); return null; },
                r =>
                {
                    if (_logForm != null && !_logForm.IsDisposed)
                    {
                        _logForm.RefreshLog();
                    }
                },
                OnDatabaseError);
        }

        private void OnDatabaseError(Exception ex)
        {
            SetStatus("Database error: " + ex.Message, true);
            if (!_dbErrorShown)
            {
                _dbErrorShown = true;
                MessageBox.Show(this,
                    "The elevator log database could not be reached. The elevators still work, " +
                    "but events are not being saved.\r\n\r\nCheck that SQL Server LocalDB is " +
                    "installed and the server name is right (" + LogRepository.ServerName + ").\r\n\r\nDetails: " + ex.Message,
                    "Database problem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SetStatus(string text, bool isError)
        {
            statusLabel.ForeColor = isError ? Color.Firebrick : SystemColors.ControlText;
            statusLabel.Text = text;
        }
    }
}
