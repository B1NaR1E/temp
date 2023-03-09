using Dvt.ElevatorSimulator.Domain.Shared.Enums;

namespace Dvt.ElevatorSimulator.Domain.Core;

public interface IElevatorCore
{
    int CurrentFloor { get; }
    int DestinationFloor { get; }
    Direction Direction { get; }
    State State { get; }
    void Move();
}