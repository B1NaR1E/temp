using Dvt.ElevatorSimulator.Domain.Core;

namespace Dvt.ElevatorSimulator.Domain.Base;

public interface IElevatorBase : IElevatorCore
{
    void AddStop(int destinationFloor);
}