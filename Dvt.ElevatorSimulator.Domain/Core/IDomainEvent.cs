using MediatR;

namespace Dvt.ElevatorSimulator.Domain.Core;

public interface IDomainEvent : INotification
{
    Guid EventId { get; }
    DateTime CreateOn { get; }
}