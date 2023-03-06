using Dvt.ElevatorSimulator.Domain.Core;

namespace Dvt.ElevatorSimulator.Domain.Base;

public abstract class EntityBase : IEntity
{
    private readonly List<BaseDomainEvent> _events;

    protected EntityBase()
    {
        _events = new List<BaseDomainEvent>();
    }

    public Guid Id { get; protected init; }
    public IReadOnlyList<IDomainEvent> Events => _events.AsReadOnly();

    protected void AddEvent(BaseDomainEvent @event)
    {
        _events.Add(@event);
    }

    protected void RemoveEvent(BaseDomainEvent @event)
    {
        _events.Remove(@event);
    }
}