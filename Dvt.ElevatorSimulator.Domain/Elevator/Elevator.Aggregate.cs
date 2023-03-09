using Dvt.ElevatorSimulator.Domain.Shared.Enums;

namespace Dvt.ElevatorSimulator.Domain.Elevator;

public partial class Elevator
{
    public bool LoadPassenger(int destinationFloor, int originatingFloor, int totalPassengers)
    {
        for (int i = 0; i < totalPassengers; i++)
        {
            _passengers.Add(new PassengerValueObject(destinationFloor, originatingFloor));
        }

        if (_passengers.Count > _maxPassengers)
        {
            State = State.OverLimit;
            return false;
        }

        AddStop(destinationFloor);
        return true;
    }

    public IEnumerable<PassengerValueObject> UnloadPassengers()
    {
        IEnumerable<PassengerValueObject> passengers;

        if (State == State.OverLimit)
        {
            State = State.Stopped;
            passengers = _passengers.Where(p => p.CurrentFloor == CurrentFloor).ToList();
            _passengers.RemoveAll(p => p.CurrentFloor == CurrentFloor);

            return passengers;
        }

        passengers = _passengers.Where(p => p.DestinationFloor == CurrentFloor).ToList();
        _passengers.RemoveAll(p => p.DestinationFloor == CurrentFloor);

        return passengers;
    }

    public int TotalPassengers()
    {
        return _passengers.Count;
    }
}