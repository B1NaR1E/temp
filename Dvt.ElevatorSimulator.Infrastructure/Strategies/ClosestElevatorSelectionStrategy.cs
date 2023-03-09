using Dvt.ElevatorSimulator.Domain.Elevator;
using Dvt.ElevatorSimulator.Domain.Shared.Enums;
using Dvt.ElevatorSimulator.Infrastructure.Interfaces;

namespace Dvt.ElevatorSimulator.Infrastructure.Strategies;

public class ClosestElevatorSelectionStrategy : ISelectionStrategy
{
    public Guid Run(IReadOnlyList<Elevator> elevators, ElevatorRequest request)
    {
        Elevator? selectedElevator = null;

        switch (request.OriginatingFloor < request.DestinationFloor)
        {
            case true:
            {
                var elevatorsGoingUp = elevators.Where(e =>
                        e.Direction == Direction.Up &&
                        e.CurrentFloor <= request.OriginatingFloor &&
                        e.State != State.OverLimit)
                    .ToList();

                if (elevatorsGoingUp.Any())
                    selectedElevator = GetClosestElevator(elevatorsGoingUp, request.OriginatingFloor);
                break;
            }
            case false:
            {
                var elevatorsGoingDown = elevators.Where(e =>
                        e.Direction == Direction.Down &&
                        e.CurrentFloor >= request.OriginatingFloor &&
                        e.State != State.OverLimit)
                    .ToList();

                if (elevatorsGoingDown.Any())
                    selectedElevator = GetClosestElevator(elevatorsGoingDown, request.OriginatingFloor);
                break;
            }
        }

        if (selectedElevator != null)
            return selectedElevator.Id;

        var staticElevators = elevators.Where(e => e.Direction == Direction.Static && e.State != State.OverLimit)
            .ToList();

        if (staticElevators.Any())
            selectedElevator = GetClosestElevator(staticElevators, request.OriginatingFloor);


        return selectedElevator?.Id ?? Guid.Empty;
    }
    
    private static Elevator GetClosestElevator(IEnumerable<Elevator> elevators, int originatingFloor)
    {
        return elevators.Aggregate((x, y) =>
            (Math.Abs((x.CurrentFloor - originatingFloor)) <
            Math.Abs((y.CurrentFloor - originatingFloor))) ? x : y);
    }
}