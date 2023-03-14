using Dvt.ElevatorSimulator.Api.DTOs;
using Dvt.ElevatorSimulator.Infrastructure;
using Dvt.ElevatorSimulator.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dvt.ElevatorSimulator.Api.Controllers;

[ApiController]
[Route("api/v1")]
public class ElevatorController : ControllerBase
{
    private readonly ISimulator _simulator;

    public ElevatorController(ISimulator simulator)
    {
        _simulator = simulator;
    }
    
    [HttpPost]
    [Route("/call_elevator")]
    public async Task<IActionResult> CallElevator([FromBody]CallElevatorRequest request)
    {
        _simulator.AddRequest(new ElevatorRequest(request.OriginatingFloor, request.DestinationFloor, request.TotalPassengers));
        
        return Ok(new CallElevatorResponse());
    }
    
    [HttpPost]
    [Route("/elevator_setup")]
    public async Task<IActionResult> ElevatorSetup([FromBody]SetupElevatorsRequest request)
    {
        _simulator.SetupElevators(request.TotalFloors, request.TotalPassengers, request.TotalElevators);
        
        return Ok(new CallElevatorResponse());
    }
}