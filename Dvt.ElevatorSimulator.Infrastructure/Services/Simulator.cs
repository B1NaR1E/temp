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

    public void SetupElevators(int totalFloors, int totalPassengers, int totalElevators)
    {
        _elevatorControlSystem.CreateElevators(totalElevators, totalFloors, totalPassengers);
    }

    public void AddRequest(ElevatorRequest request)
    {
        _elevatorRequests.Add(request);
    }

    public IReadOnlyList<Elevator> GetElevators()
    {
        return _elevatorControlSystem.Elevators;
    }

    public void Step()
    {
        if (_elevatorRequests.Any())
        {
            var request = _elevatorRequests.First();
            var result = _elevatorControlSystem.ProcessRequest(request);

            if (result)
                _elevatorRequests.Remove(request);
        }
        
        _elevatorControlSystem.Elevators.ToList().ForEach(e => 
        {
            if ((e.State is State.Stopped or State.OverLimit) && e.TotalPassengers() > 0 && !e.IsBusy)
            {
                e.UnloadPassengers();
            }
            
            if (e.State != State.OverLimit || !e.IsBusy)
            {
                e.IsBusy = true;
                var loadPassengerJobs =  _elevatorControlSystem.ElevatorJobs[e.Id].Where(j => j.OriginatingFloor == e.CurrentFloor).ToList();

                foreach (var job in loadPassengerJobs)
                {
                    var passengersLoadedSuccessfully = e.LoadPassenger(job.DestinationFloor, job.OriginatingFloor, job.TotalPassengers);
                    
                    if (!passengersLoadedSuccessfully)
                        _elevatorRequests.Add(job);
                    
                    _elevatorControlSystem.ElevatorJobs[e.Id].Remove(job);
                }

                e.IsBusy = false;
            }
            
            e.Move();
        });
    }
}