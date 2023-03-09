using Dvt.ElevatorSimulator.Domain.Base.Elevator;

namespace Dvt.ElevatorSimulator.Domain.Elevator;

public partial class Elevator : ElevatorBase
{
    private readonly List<PassengerValueObject> _passengers;

    public Elevator(int totalFloors, int totalPassengers) : base(totalFloors, totalPassengers)
    {
        _passengers = new List<PassengerValueObject>();
    }
}