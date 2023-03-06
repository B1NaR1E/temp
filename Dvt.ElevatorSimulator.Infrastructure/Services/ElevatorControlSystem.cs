using Dvt.ElevatorSimulator.Domain.Elevator;

namespace Dvt.ElevatorSimulator.Infrastructure.Services;

public class ElevatorControlSystem : IElevatorControlSystem
{
    private readonly List<ElevatorRequest> _requests;
    private readonly List<Elevator> _elevators;
    private readonly ISelectionStrategy _selectionStrategy;

    public ElevatorControlSystem(ISelectionStrategy selectionStrategy)
    {
        _requests = new List<ElevatorRequest>();
        _elevators = new List<Elevator>();
        _selectionStrategy = selectionStrategy;
    }

    public IReadOnlyList<ElevatorRequest> Requests => _requests.AsReadOnly();
    public IReadOnlyList<Elevator> Elevators => _elevators.AsReadOnly();

    public void AddRequests(ElevatorRequest request)
    {
        _requests.Add(request);
    }

    public void CreateElevators(int totalElevators, int totalFloors, int maxPassengersPerElevator)
    {
        for (int i = 0; i < totalElevators; i++)
        {
            _elevators.Add(new Elevator(totalFloors, maxPassengersPerElevator));
        }
    }

    public bool ProcessRequest(ElevatorRequest request)
    {
        bool successful = false;
        
        var elevatorId = _selectionStrategy.Run(Elevators, request);
        var elevator = Elevators.FirstOrDefault(e => e.Id == elevatorId);
        
        if (elevator is not null)
        {
            //If elevator is found the system loads the request on the elevator job queue.
            //_elevatorJobs[elevatorId].Add(request);
                
            //Adds the floor of the request to the elevator's stops
            elevator.AddStops(request.OriginatingFloor);
                
            //If request is handed off to the elevator the system removes the request from it's queue.
            //_elevatorCalls.Remove(request);
        }

        return successful;
    }
}