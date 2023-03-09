using Dvt.ElevatorSimulator.Domain.Elevator;

namespace Dvt.ElevatorSimulator.Infrastructure.Interfaces;

public interface ISelectionStrategy
{
    Guid Run(IReadOnlyList<Elevator> elevators, ElevatorRequest request);
}