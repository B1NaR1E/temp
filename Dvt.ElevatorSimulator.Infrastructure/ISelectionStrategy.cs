using Dvt.ElevatorSimulator.Domain.Elevator;

namespace Dvt.ElevatorSimulator.Infrastructure;

public interface ISelectionStrategy
{
    Guid Run(IReadOnlyList<Elevator> elevators, ElevatorRequest request);
}