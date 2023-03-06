namespace Dvt.ElevatorSimulator.Domain.Core;

public interface IPassenger
{
    int CurrentFloor { get; }
    int DestinationFloor { get; set; }
}