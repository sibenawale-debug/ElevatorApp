namespace ElevatorApp
{
    public enum ElevatorStatus { Idle, Moving, DoorsOpen, DoorsClosing, Stopped, Recovering }

    public enum ElevatorEventKind { Message, StateChanged, Arrived, AlarmSounded, Countdown }

    /// <summary>What an elevator tells its observers.</summary>
    public class ElevatorEvent
    {
        public ElevatorEvent(ElevatorEventKind kind, string message, int value, ElevatorStatus status)
        {
            Kind = kind;
            Message = message;
            Value = value;
            Status = status;
        }

        public ElevatorEventKind Kind { get; }
        public string Message { get; }   // Message events: the text to log
        public int Value { get; }        // Arrived: the floor / Countdown: seconds left
        public ElevatorStatus Status { get; }
    }

    /// <summary>Observer pattern: anything that wants to follow an elevator implements this.</summary>
    public interface IElevatorObserver
    {
        void Update(Elevator elevator, ElevatorEvent e);
    }
}
