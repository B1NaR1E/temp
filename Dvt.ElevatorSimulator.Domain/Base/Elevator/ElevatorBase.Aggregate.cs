using Dvt.ElevatorSimulator.Domain.Shared.Enums;

namespace Dvt.ElevatorSimulator.Domain.Base.Elevator;

public abstract partial class ElevatorBase
{
    public void Move()
    {
        DestinationFloor = GetDestination();

        if (DestinationFloor == CurrentFloor || State == State.OverLimit) 
            return;
        
        State = State.Moving;
        if (Direction == Direction.Up)
        {
            if(CurrentFloor != _totalFloors)
                ++CurrentFloor;
        }
        else
        {
            if(CurrentFloor != 1)
                --CurrentFloor;
        }

        if (CurrentFloor != DestinationFloor) 
            return;
        
        State = State.Stopped;
        Stops.Remove(CurrentFloor);
    }
    
    public void AddStop(int destinationFloor)
    {
        if(!Stops.Contains(destinationFloor))
            Stops.Add(destinationFloor);
    }
    
    private int GetDestination()
    {
        return Stops.Any() ? Stops.MinBy(s => Math.Abs(s - CurrentFloor)) : DestinationFloor;
    }
}