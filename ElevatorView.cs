using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace ElevatorApp
{
    /// <summary>
    /// Everything you see for ONE elevator: the shaft with the car, the two floor displays and
    /// the elevator's own control panel. It observes its Elevator (Observer pattern) and
    /// supplies the animation delegates (timers) the Elevator calls.
    /// </summary>
    public partial class ElevatorView : UserControl, IElevatorObserver
    {
        private const int FloorHeight = 150;
        private const int CarWidth = 110;
        private const int CarHeight = 130;
        private const int MoveStep = 2;        // pixels per timer tick
        private const double DoorStep = 0.1;   // fraction of door travel per timer tick

        private Elevator _elevator;

        private readonly Panel[] _floorDoor = new Panel[Elevator.TopFloor + 1];
        private readonly Label[] _floorDisplay = new Label[Elevator.TopFloor + 1];
        private readonly Button[] _panelButton = new Button[Elevator.TopFloor + 1];

        private readonly Timer _moveTimer = new Timer { Interval = 20 };
        private readonly Timer _doorTimer = new Timer { Interval = 40 };
        private int _moveTargetTop;
        private Action _moveDone;
        private bool _doorsOpening;
        private double _doorPct;               // 0 = closed, 1 = fully open
        private Action _doorDone;

        private int _shownFloor = Elevator.BottomFloor;
        private string _shownArrow = "";

        public ElevatorView()
        {
            InitializeComponent();
            _floorDoor[1] = floorDoor1;
            _floorDoor[2] = floorDoor2;
            _floorDisplay[1] = floorDisplay1;
            _floorDisplay[2] = floorDisplay2;
            _panelButton[1] = panelButton1;
            _panelButton[2] = panelButton2;
            bannerLabel.Visible = false;

            _moveTimer.Tick += MoveTimer_Tick;
            _doorTimer.Tick += DoorTimer_Tick;
            Disposed += (s, e) => { _moveTimer.Dispose(); _doorTimer.Dispose(); };
        }

        /// <summary>Connects this view to its elevator.</summary>
        public void Bind(Elevator elevator, string title)
        {
            _elevator = elevator;
            titleLabel.Text = title;
            _shownFloor = elevator.CurrentFloor;

            elevator.MoveAnimator = AnimateMove;
            elevator.DoorAnimator = AnimateDoors;
            elevator.HaltAnimations = HaltAnimations;
            elevator.Attach(this);

            carPanel.Top = CarTop(elevator.CurrentFloor);
            ApplyDoors();
        }

        // =====================================================================
        // buttons of this elevator's control panel (only this elevator handles them)
        // =====================================================================

        private void panelButton1_Click(object sender, EventArgs e) { PressPanelButton(panelButton1, 1); }
        private void panelButton2_Click(object sender, EventArgs e) { PressPanelButton(panelButton2, 2); }
        private void openDoorButton_Click(object sender, EventArgs e) { _elevator.OpenDoorButton(); }
        private void closeDoorButton_Click(object sender, EventArgs e) { _elevator.CloseDoorButton(); }
        private void stopButton_Click(object sender, EventArgs e) { _elevator.Stop(NearestFloor()); }
        private void alarmButton_Click(object sender, EventArgs e) { _elevator.Alarm(NearestFloor()); }

        private void PressPanelButton(Button button, int floor)
        {
            SetLight(button, true);
            if (!_elevator.PanelRequest(floor))
            {
                SetLight(button, false); // refused (e.g. elevator stopped)
            }
        }

        private static void SetLight(Button button, bool on)
        {
            button.BackColor = on ? Color.Gold : SystemColors.Control;
        }

        // =====================================================================
        // Observer: reacts to what the elevator reports
        // =====================================================================

        public void Update(Elevator elevator, ElevatorEvent e)
        {
            switch (e.Kind)
            {
                case ElevatorEventKind.Arrived:
                    ShowFloor(e.Value, "");
                    SetLight(_panelButton[e.Value], false);
                    break;

                case ElevatorEventKind.StateChanged:
                    OnStatusChanged(e.Status);
                    break;

                case ElevatorEventKind.AlarmSounded:
                    SystemSounds.Hand.Play();
                    break;

                case ElevatorEventKind.Countdown:
                    bannerLabel.Text = "ALARM - returning to the nearest floor in " + e.Value + " s";
                    bannerLabel.Visible = true;
                    SystemSounds.Beep.Play();
                    break;
            }
        }

        private void OnStatusChanged(ElevatorStatus status)
        {
            if (status == ElevatorStatus.Stopped)
            {
                SetLight(panelButton1, false);
                SetLight(panelButton2, false);
                bannerLabel.Text = "EMERGENCY STOP - press ALARM to go to the nearest floor";
                bannerLabel.Visible = true;
            }
            else if (status == ElevatorStatus.Recovering)
            {
                bannerLabel.Text = "ALARM - moving to the nearest floor...";
                bannerLabel.Visible = true;
            }
            else
            {
                bannerLabel.Visible = false;
            }
            RefreshDisplays();
        }

        /// <summary>Single place that updates every display of this elevator.</summary>
        private void ShowFloor(int floor, string arrow)
        {
            _shownFloor = floor;
            _shownArrow = arrow;
            RefreshDisplays();
        }

        private void RefreshDisplays()
        {
            if (_elevator == null)
            {
                return;
            }

            string text = _elevator.Status == ElevatorStatus.Stopped ? "STOP" : _shownFloor + _shownArrow;
            panelDisplayLabel.Text = text;
            for (int f = Elevator.BottomFloor; f <= Elevator.TopFloor; f++)
            {
                _floorDisplay[f].Text = text;
                bool open = f == _elevator.CurrentFloor && _doorPct > 0;
                _floorDoor[f].BackColor = open ? Color.SeaGreen : Color.Firebrick;
            }
        }

        // =====================================================================
        // animation with delegates and timers
        // =====================================================================

        private static int CarTop(int floor)
        {
            return (Elevator.TopFloor - floor) * FloorHeight + 10;
        }

        private int NearestFloor()
        {
            return Math.Abs(carPanel.Top - CarTop(Elevator.TopFloor)) <
                   Math.Abs(carPanel.Top - CarTop(Elevator.BottomFloor))
                   ? Elevator.TopFloor : Elevator.BottomFloor;
        }

        private void HaltAnimations()
        {
            _moveTimer.Stop();
            _doorTimer.Stop();
            _moveDone = null;
            _doorDone = null;
        }

        private void AnimateMove(int floor, Action done)
        {
            _moveTargetTop = CarTop(floor);
            _moveDone = done;
            _moveTimer.Start();
        }

        private void MoveTimer_Tick(object sender, EventArgs e)
        {
            int diff = _moveTargetTop - carPanel.Top;
            if (Math.Abs(diff) <= MoveStep)
            {
                carPanel.Top = _moveTargetTop;
                _moveTimer.Stop();
                Action done = _moveDone;
                _moveDone = null;
                if (done != null) done();
                return;
            }

            carPanel.Top += Math.Sign(diff) * MoveStep;
            ShowFloor(NearestFloor(), diff < 0 ? " ▲" : " ▼");
        }

        private void AnimateDoors(bool open, Action done)
        {
            _doorsOpening = open;
            _doorDone = done;
            _doorTimer.Start();
        }

        private void DoorTimer_Tick(object sender, EventArgs e)
        {
            _doorPct += _doorsOpening ? DoorStep : -DoorStep;
            bool finished = false;
            if (_doorPct >= 1) { _doorPct = 1; finished = true; }
            if (_doorPct <= 0) { _doorPct = 0; finished = true; }
            ApplyDoors();

            if (finished)
            {
                _doorTimer.Stop();
                Action done = _doorDone;
                _doorDone = null;
                if (done != null) done();
            }
        }

        private void ApplyDoors()
        {
            int half = (int)(CarWidth / 2.0 * (1 - _doorPct));
            doorLeftPanel.SetBounds(0, 0, half, CarHeight);
            doorRightPanel.SetBounds(CarWidth - half, 0, half, CarHeight);
            RefreshDisplays();
        }
    }
}
