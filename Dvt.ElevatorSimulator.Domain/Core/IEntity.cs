namespace Dvt.ElevatorSimulator.Domain.Core;

public interface IEntity
{
    Guid Id { get; }
    IReadOnlyList<IDomainEvent> Events { get; }
}