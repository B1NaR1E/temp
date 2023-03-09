using Dvt.ElevatorSimulator.Domain.Elevator;
using Dvt.ElevatorSimulator.Domain.Shared.Enums;
using Dvt.ElevatorSimulator.Infrastructure.Interfaces;

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
        _elevatorControlSystem.AddRequests(request);
    }

    public IReadOnlyList<Elevator> GetElevators()
    {
        return _elevatorControlSystem.Elevators;
    }

    public void Step()
    {
        _elevatorControlSystem.Elevators.ToList().ForEach(e => 
        {
            if(e.TotalPassengers() > 0)
            {
                e.UnloadPassengers();
            }

            var loadPassengerJobs = _elevatorControlSystem.ElevatorJobs[e.Id].Where(j => j.OriginatingFloor == e.CurrentFloor).ToList();

            foreach (var job in loadPassengerJobs)
            {
                e.LoadPassenger(job.DestinationFloor, job.OriginatingFloor, job.TotalPassengers);

                if(e.State is not State.OverLimit)
                {
                    _elevatorControlSystem.ElevatorJobs[e.Id].Remove(job);
                }
            }

            e.Move();
        });
    }
}