using Dvt.ElevatorSimulator.Domain.Elevator;

namespace Dvt.ElevatorSimulator.Infrastructure;

public interface IElevatorControlSystem
{
    IReadOnlyList<ElevatorRequest> Requests { get; }
    IReadOnlyList<Elevator> Elevators { get; }
    void AddRequests(ElevatorRequest request);
    void CreateElevators(int totalElevators, int totalFloors, int maxPassengersPerElevator);
}