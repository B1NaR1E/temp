using System.Security.Cryptography;
using Dvt.ElevatorSimulator.Domain.Enums;

namespace Dvt.ElevatorSimulator.Domain.Base.Elevator;

public abstract partial class ElevatorBase : EntityBase, IElevatorBase
{
    private readonly int _totalFloors;

    protected ElevatorBase(int totalFloors, int totalPassengers)
    {
        _totalFloors = totalFloors;
        Stops = new List<int>();
        Id = new Guid();
        State = State.Stopped;
        CurrentFloor = 1;
        DestinationFloor = 1;
    }

    private List<int> Stops { get; }
    public int CurrentFloor { get; private set; }
    public int DestinationFloor { get; private set; }
    
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
    
    public State State { get; private set; }
}