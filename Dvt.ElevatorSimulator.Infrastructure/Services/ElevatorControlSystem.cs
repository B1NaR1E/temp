using Dvt.ElevatorSimulator.Domain.Elevator;
using Dvt.ElevatorSimulator.Infrastructure.Interfaces;

namespace Dvt.ElevatorSimulator.Infrastructure.Services;

public class ElevatorControlSystem : IElevatorControlSystem
{
    //private readonly List<ElevatorRequest> _requests;
    private readonly List<Elevator> _elevators;
    private readonly ISelectionStrategy _selectionStrategy;

    public Dictionary<Guid, List<ElevatorRequest>> ElevatorJobs { get; private set; }

    public ElevatorControlSystem(ISelectionStrategy selectionStrategy)
    {
        //_requests = new List<ElevatorRequest>();
        _elevators = new List<Elevator>();
        ElevatorJobs = new Dictionary<Guid, List<ElevatorRequest>>();

        _selectionStrategy = selectionStrategy;
    }

    //public IReadOnlyList<ElevatorRequest> Requests => _requests.AsReadOnly();
    public IReadOnlyList<Elevator> Elevators => _elevators.AsReadOnly();

    public void AddRequests(ElevatorRequest request)
    {
        //_requests.Add(request);
        
    }

    public void CreateElevators(int totalElevators, int totalFloors, int maxPassengersPerElevator)
    {
        for (int i = 0; i < totalElevators; i++)
        {
            var elevator = new Elevator(totalFloors, maxPassengersPerElevator);
            _elevators.Add(elevator);
            ElevatorJobs.Add(elevator.Id, new List<ElevatorRequest>());
        }
    }

    public bool ProcessRequest(ElevatorRequest request)
    {
        bool successful = false;
        
        var elevatorId = _selectionStrategy.Run(Elevators, request);
        var elevator = Elevators.FirstOrDefault(e => e.Id == elevatorId);
        
        if (elevator is not null)
        {
            ElevatorJobs[elevatorId].Add(request);
               
            elevator.AddStop(request.OriginatingFloor);

            //_requests.Remove(request);
        }

        return successful;
    }
}