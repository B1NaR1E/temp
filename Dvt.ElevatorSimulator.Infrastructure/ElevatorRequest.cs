

namespace Dvt.ElevatorSimulator.Infrastructure;

public class ElevatorRequest
{
    public ElevatorRequest(int originatingFloor, int destinationFloor, int totalPassengers)
    {
        OriginatingFloor = originatingFloor;
        DestinationFloor = destinationFloor;
        TotalPassengers = totalPassengers;
    }

    public int OriginatingFloor { get; }
    public int DestinationFloor { get; }
    public int TotalPassengers { get; }
}