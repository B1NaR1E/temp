using Dvt.ElevatorSimulator.Domain.Enums;

namespace Dvt.ElevatorSimulator.Domain.Core;

public interface IElevator
{
    int CurrentFloor { get; }
    int DestinationFloor { get; }
    Direction Direction { get; }
    State State { get; }
    void Move();
}