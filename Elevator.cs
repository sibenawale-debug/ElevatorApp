using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ElevatorApp
{
    /// <summary>
    /// One elevator. It is the "context" of the State pattern (all behaviour is delegated to the
    /// current ElevatorState) and the "subject" of the Observer pattern (it notifies observers).
    /// It knows nothing about controls: the view plugs in the animation delegates.
    /// </summary>
    public class Elevator
    {
        public const int BottomFloor = 1;
        public const int TopFloor = 2;
        public const int RecoverySeconds = 5;
        private const int DwellMilliseconds = 2500;

        private readonly List<IElevatorObserver> _observers = new List<IElevatorObserver>();
        private readonly List<int> _queue = new List<int>();
        private readonly Timer _dwellTimer = new Timer { Interval = DwellMilliseconds };
        private readonly Timer _recoveryTimer = new Timer { Interval = 1000 };
        private ElevatorState _state;

        public Elevator(string name, int startFloor)
        {
            Name = name;
            CurrentFloor = startFloor;
            _dwellTimer.Tick += (s, e) => _state.DwellElapsed();
            _recoveryTimer.Tick += (s, e) => _state.RecoveryTick();
            _state = new IdleState(this);
        }

        public string Name { get; }
        public int CurrentFloor { get; internal set; }
        public ElevatorStatus Status { get { return _state.Status; } }

        /// <summary>Animate the car to a floor, then invoke the callback.</summary>
        public Action<int, Action> MoveAnimator { get; set; }

        /// <summary>Animate the doors (true = open, false = close), then invoke the callback.</summary>
        public Action<bool, Action> DoorAnimator { get; set; }

        /// <summary>Freeze every running animation immediately (and drop their callbacks).</summary>
        public Action HaltAnimations { get; set; }

        // ---------------- Observer pattern ----------------

        public void Attach(IElevatorObserver observer) { _observers.Add(observer); }
        public void Detach(IElevatorObserver observer) { _observers.Remove(observer); }

        private void Notify(ElevatorEvent e)
        {
            foreach (IElevatorObserver observer in _observers.ToArray())
            {
                observer.Update(this, e);
            }
        }

        internal void Say(string message)
        {
            Notify(new ElevatorEvent(ElevatorEventKind.Message, message, 0, Status));
        }

        internal void NotifyArrived()
        {
            Notify(new ElevatorEvent(ElevatorEventKind.Arrived, null, CurrentFloor, Status));
        }

        internal void NotifyAlarm()
        {
            Notify(new ElevatorEvent(ElevatorEventKind.AlarmSounded, null, 0, Status));
        }

        internal void NotifyCountdown(int secondsLeft)
        {
            Notify(new ElevatorEvent(ElevatorEventKind.Countdown, null, secondsLeft, Status));
        }

        // ---------------- requests and buttons (delegated to the current state) ----------------

        /// <returns>false when the request was refused.</returns>
        public bool Request(int floor, string source)
        {
            if (floor < BottomFloor || floor > TopFloor)
            {
                return false;
            }
            return _state.Request(floor, source);
        }

        /// <summary>Request made from this elevator's own control panel.</summary>
        public bool PanelRequest(int floor) { return Request(floor, "Control panel button"); }

        /// <summary>Request made from this elevator's own outside call button (on a floor).</summary>
        public bool HallRequest(int floor) { return Request(floor, "Request button"); }

        public void OpenDoorButton() { _state.OpenDoorButton(); }
        public void CloseDoorButton() { _state.CloseDoorButton(); }
        public void Stop(int nearestFloor) { _state.Stop(nearestFloor); }
        public void Alarm(int nearestFloor) { _state.Alarm(nearestFloor); }



        // ---------------- helpers used by the states ----------------

        internal bool IsCurrent(ElevatorState state) { return ReferenceEquals(_state, state); }

        internal void SetState(ElevatorState state)
        {
            _state = state;
            Notify(new ElevatorEvent(ElevatorEventKind.StateChanged, null, 0, state.Status));
            state.Enter();
        }

        internal bool QueueContains(int floor) { return _queue.Contains(floor); }
        internal void QueueAdd(int floor) { _queue.Add(floor); }
        internal void QueueClear() { _queue.Clear(); }
        internal List<int> QueueSnapshot() { return new List<int>(_queue); }

        /// <summary>Takes the next waiting stop and either opens the doors (already there) or moves.</summary>
        internal void StartNext()
        {
            if (_queue.Count == 0)
            {
                return;
            }

            int target = _queue[0];
            _queue.RemoveAt(0);
            if (target == CurrentFloor)
            {
                SetState(new DoorsOpenState(this));
            }
            else
            {
                SetState(new MovingState(this, target));
            }
        }

        internal void RunMove(int floor, Action done)
        {
            if (MoveAnimator != null) MoveAnimator(floor, done); else done();
        }

        internal void RunDoors(bool open, Action done)
        {
            if (DoorAnimator != null) DoorAnimator(open, done); else done();
        }

        internal void Halt()
        {
            if (HaltAnimations != null) HaltAnimations();
            StopDwell();
            StopRecoveryTimer();
        }

        internal bool DwellRunning { get { return _dwellTimer.Enabled; } }
        internal void StartDwell() { _dwellTimer.Stop(); _dwellTimer.Start(); }
        internal void StopDwell() { _dwellTimer.Stop(); }
        internal void RestartDwell() { if (_dwellTimer.Enabled) StartDwell(); }
        internal void StartRecoveryTimer() { _recoveryTimer.Start(); }
        internal void StopRecoveryTimer() { _recoveryTimer.Stop(); }
    }
}
