using Dvt.ElevatorSimulator.Domain.Elevator;

namespace Dvt.ElevatorSimulator.Infrastructure.Interfaces;

public interface IElevatorControlSystem
{
    //IReadOnlyList<ElevatorRequest> Requests { get; }
    IReadOnlyList<Elevator> Elevators { get; }
    Dictionary<Guid, List<ElevatorRequest>> ElevatorJobs { get; }
    void AddRequests(ElevatorRequest request);
    bool ProcessRequest(ElevatorRequest request);
    void CreateElevators(int totalElevators, int totalFloors, int maxPassengersPerElevator);
}