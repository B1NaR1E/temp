using Dvt.ElevatorSimulator.Domain.Core;

namespace Dvt.ElevatorSimulator.Domain.Base;

public interface IElevatorBase : IElevator
{
    void AddStops(int destinationFloor);
}