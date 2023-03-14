namespace Dvt.ElevatorSimulator.Api.DTOs;

public class SetupElevatorsRequest
{
    public int TotalElevators { get; set; }
    public int TotalFloors { get; set; }
    public int TotalPassengers { get; set; }
}