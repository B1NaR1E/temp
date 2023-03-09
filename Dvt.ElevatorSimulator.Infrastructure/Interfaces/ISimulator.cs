using Dvt.ElevatorSimulator.Domain.Elevator;

namespace Dvt.ElevatorSimulator.Infrastructure.Interfaces;
public interface ISimulator
{
    void AddRequest(ElevatorRequest request);
    IReadOnlyList<Elevator> GetElevators();
    void Step();
}