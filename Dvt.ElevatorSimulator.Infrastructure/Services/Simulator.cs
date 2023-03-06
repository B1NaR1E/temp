using Dvt.ElevatorSimulator.Domain.Elevator;

namespace Dvt.ElevatorSimulator.Infrastructure.Services;

public class Simulator : ISimulator
{
    private readonly List<ElevatorRequest> _elevatorRequests;
    private IElevatorControlSystem _elevatorControlSystem;

    public Simulator(IElevatorControlSystem elevatorControlSystem)
    {
        _elevatorControlSystem = elevatorControlSystem;
        _elevatorRequests = new List<ElevatorRequest>();
    }

    public void AddRequest(ElevatorRequest request)
    {
        _elevatorRequests.Add(request);
    }

    public IReadOnlyList<Elevator> GetElevators()
    {
        return _elevatorControlSystem.Elevators;
    }
}