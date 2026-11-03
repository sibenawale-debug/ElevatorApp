namespace ElevatorApp
{
    /// <summary>
    /// State pattern: each state decides what every button/request does and which state comes next.
    /// </summary>
    internal abstract class ElevatorState
    {
        protected readonly Elevator Ctx;

        protected ElevatorState(Elevator ctx) { Ctx = ctx; }

        public abstract ElevatorStatus Status { get; }

        public virtual void Enter() { }
        public abstract bool Request(int floor, string source);

        public virtual void OpenDoorButton() { }
        public virtual void CloseDoorButton() { }
        public virtual void DwellElapsed() { }
        public virtual void RecoveryTick() { }

        /// <summary>STOP freezes the car, drops all waiting requests and enters the Stopped state.</summary>
        public virtual void Stop(int nearestFloor)
        {
            Ctx.Halt();
            Ctx.QueueClear();
            Ctx.SetState(new StoppedState(Ctx, nearestFloor));
        }

        public virtual void Alarm(int nearestFloor)
        {
            Ctx.Say("ALARM triggered");
            Ctx.NotifyAlarm();
        }

        protected bool Enqueue(int floor, string source)
        {
            if (!Ctx.QueueContains(floor))
            {
                Ctx.Say(source + ": floor " + floor);
                Ctx.QueueAdd(floor);
            }
            return true;
        }
    }

    internal sealed class IdleState : ElevatorState
    {
        public IdleState(Elevator ctx) : base(ctx) { }
        public override ElevatorStatus Status { get { return ElevatorStatus.Idle; } }

        public override void Enter() { Ctx.StartNext(); }

        public override bool Request(int floor, string source)
        {
            if (floor == Ctx.CurrentFloor)
            {
                Ctx.Say(source + ": floor " + floor + " (already here)");
                Ctx.SetState(new DoorsOpenState(Ctx));
                return true;
            }
            Enqueue(floor, source);
            Ctx.StartNext();
            return true;
        }

        public override void OpenDoorButton()
        {
            Ctx.Say("Door button: open");
            Ctx.SetState(new DoorsOpenState(Ctx));
        }
    }

    internal sealed class MovingState : ElevatorState
    {
        private readonly int _target;

        public MovingState(Elevator ctx, int target) : base(ctx) { _target = target; }
        public override ElevatorStatus Status { get { return ElevatorStatus.Moving; } }

        public override void Enter()
        {
            Ctx.Say("Moving to floor " + _target);
            Ctx.RunMove(_target, () =>
            {
                if (!Ctx.IsCurrent(this)) return;
                Ctx.CurrentFloor = _target;
                Ctx.Say("Arrived at floor " + _target);
                Ctx.SetState(new DoorsOpenState(Ctx));
            });
        }

        public override bool Request(int floor, string source)
        {
            if (floor == _target)
            {
                return true; // already on its way
            }
            return Enqueue(floor, source);
        }

        public override void Alarm(int nearestFloor)
        {
            Stop(nearestFloor);          // become Stopped first ...
            Ctx.Alarm(nearestFloor);     // ... then let the Stopped state run the alarm countdown
        }
    }

    internal sealed class DoorsOpenState : ElevatorState
    {
        public DoorsOpenState(Elevator ctx) : base(ctx) { }
        public override ElevatorStatus Status { get { return ElevatorStatus.DoorsOpen; } }

        public override void Enter()
        {
            Ctx.StopDwell();
            Ctx.NotifyArrived();
            Ctx.Say("Doors opening");
            Ctx.RunDoors(true, () =>
            {
                if (!Ctx.IsCurrent(this)) return;
                Ctx.Say("Doors open");
                Ctx.StartDwell();
            });
        }

        public override bool Request(int floor, string source)
        {
            if (floor == Ctx.CurrentFloor)
            {
                Ctx.Say(source + ": floor " + floor + " (doors already open)");
                Ctx.RestartDwell();
                Ctx.NotifyArrived(); // lets the views switch the button lights off
                return true;
            }
            return Enqueue(floor, source);
        }

        public override void OpenDoorButton()
        {
            Ctx.Say("Door button: open (kept open)");
            Ctx.RestartDwell();
        }

        public override void CloseDoorButton()
        {
            if (Ctx.DwellRunning)
            {
                Ctx.Say("Door button: close");
                Ctx.SetState(new DoorsClosingState(Ctx));
            }
        }

        public override void DwellElapsed()
        {
            Ctx.StopDwell();
            Ctx.SetState(new DoorsClosingState(Ctx));
        }
    }

    internal sealed class DoorsClosingState : ElevatorState
    {
        public DoorsClosingState(Elevator ctx) : base(ctx) { }
        public override ElevatorStatus Status { get { return ElevatorStatus.DoorsClosing; } }

        public override void Enter()
        {
            Ctx.Say("Doors closing");
            Ctx.RunDoors(false, () =>
            {
                if (!Ctx.IsCurrent(this)) return;
                Ctx.Say("Doors closed");
                Ctx.SetState(new IdleState(Ctx));
            });
        }

        public override bool Request(int floor, string source)
        {
            if (floor == Ctx.CurrentFloor)
            {
                Ctx.Say(source + ": floor " + floor + " (doors reopening)");
                Ctx.SetState(new DoorsOpenState(Ctx));
                return true;
            }
            return Enqueue(floor, source);
        }

        public override void OpenDoorButton()
        {
            Ctx.Say("Door button: open");
            Ctx.SetState(new DoorsOpenState(Ctx));
        }
    }

    internal sealed class StoppedState : ElevatorState
    {
        private readonly int _nearestFloor;
        private int _remaining;
        private bool _counting;

        public StoppedState(Elevator ctx, int nearestFloor) : base(ctx) { _nearestFloor = nearestFloor; }
        public override ElevatorStatus Status { get { return ElevatorStatus.Stopped; } }

        public override void Enter()
        {
            Ctx.Say("STOP pressed - car halted, requests cleared");
        }

        public override bool Request(int floor, string source)
        {
            Ctx.Say("Ignored " + source + ": floor " + floor + " (elevator is stopped)");
            return false;
        }

        public override void Stop(int nearestFloor) { }

        public override void Alarm(int nearestFloor)
        {
            base.Alarm(nearestFloor);
            if (_counting)
            {
                return;
            }
            _counting = true;
            _remaining = Elevator.RecoverySeconds;
            Ctx.NotifyCountdown(_remaining);
            Ctx.StartRecoveryTimer();
        }

        public override void RecoveryTick()
        {
            _remaining--;
            if (_remaining > 0)
            {
                Ctx.NotifyCountdown(_remaining);
                return;
            }
            Ctx.StopRecoveryTimer();
            Ctx.SetState(new RecoveringState(Ctx, _nearestFloor));
        }
    }

    internal sealed class RecoveringState : ElevatorState
    {
        private readonly int _floor;

        public RecoveringState(Elevator ctx, int floor) : base(ctx) { _floor = floor; }
        public override ElevatorStatus Status { get { return ElevatorStatus.Recovering; } }

        public override void Enter()
        {
            Ctx.Say("Recovering to floor " + _floor);
            Ctx.RunDoors(false, () =>
            {
                if (!Ctx.IsCurrent(this)) return;
                Ctx.RunMove(_floor, () =>
                {
                    if (!Ctx.IsCurrent(this)) return;
                    Ctx.CurrentFloor = _floor;
                    Ctx.Say("Arrived at floor " + _floor);
                    Ctx.SetState(new DoorsOpenState(Ctx)); // back to normal operation
                });
            });
        }

        public override bool Request(int floor, string source)
        {
            Ctx.Say("Ignored " + source + ": floor " + floor + " (elevator is recovering)");
            return false;
        }

        public override void Stop(int nearestFloor) { }
    }
}
