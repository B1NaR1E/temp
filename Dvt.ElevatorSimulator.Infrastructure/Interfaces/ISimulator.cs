using Dvt.ElevatorSimulator.Domain.Elevator;

namespace Dvt.ElevatorSimulator.Infrastructure.Interfaces;
public interface ISimulator
{
    void AddRequest(ElevatorRequest request);
    void SetupElevators(int totalFloors, int totalPassengers, int totalElevators);
    IReadOnlyList<Elevator> GetElevators();
    void Step();
}