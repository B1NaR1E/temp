namespace Dvt.ElevatorSimulator.Api.DTOs;

public class CallElevatorRequest
{
    public int OriginatingFloor { get; set; }
    public int DestinationFloor { get; set; }
    public int TotalPassengers { get; set; }
}