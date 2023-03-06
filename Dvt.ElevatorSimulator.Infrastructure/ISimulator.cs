using Dvt.ElevatorSimulator.Domain.Elevator;

namespace Dvt.ElevatorSimulator.Infrastructure;
public interface ISimulator
{
    void AddRequest(ElevatorRequest request);
    IReadOnlyList<Elevator> GetElevators();
}