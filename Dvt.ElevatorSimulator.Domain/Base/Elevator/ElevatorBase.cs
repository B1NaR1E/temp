using Dvt.ElevatorSimulator.Domain.Shared.Enums;

namespace Dvt.ElevatorSimulator.Domain.Base.Elevator;

public abstract partial class ElevatorBase : EntityBase, IElevatorBase
{
    private readonly int _totalFloors;
    protected readonly int _maxPassengers;

    protected ElevatorBase(int totalFloors, int maxPassengers)
    {
        _totalFloors = totalFloors;
        _maxPassengers = maxPassengers;

        Stops = new List<int>();
        Id = Guid.NewGuid();
        State = State.Stopped;
        CurrentFloor = 1;
        DestinationFloor = 1;
    }

    public List<int> Stops { get; protected set; }
    public int CurrentFloor { get; protected set; }
    public int DestinationFloor { get; protected set; }
    
    public Direction Direction {
        get
        {
            if (CurrentFloor < DestinationFloor)
                return Direction.Up;
            if (CurrentFloor == DestinationFloor && !Stops.Any())
                return Direction.Static;
            
            return Direction.Down;
        }
    }
    
    public State State { get; protected set; }
}