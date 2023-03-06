using Dvt.ElevatorSimulator.Domain.Core;

namespace Dvt.ElevatorSimulator.Domain.Base;

public class BaseDomainEvent : IDomainEvent
{
    public BaseDomainEvent()
    {
        EventId = Guid.NewGuid();
        CreateOn = DateTime.Now;
    }

    public Guid EventId { get; }
    public DateTime CreateOn { get; }
}