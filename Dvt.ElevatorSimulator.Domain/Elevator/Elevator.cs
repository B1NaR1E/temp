using Dvt.ElevatorSimulator.Domain.Base.Elevator;

namespace Dvt.ElevatorSimulator.Domain.Elevator;

public partial class Elevator : ElevatorBase
{
    public Elevator(int totalFloors, int totalPassengers) : base(totalFloors, totalPassengers)
    {
        
    }
}