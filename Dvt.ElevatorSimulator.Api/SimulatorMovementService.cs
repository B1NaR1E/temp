using Dvt.ElevatorSimulator.Infrastructure;

namespace Dvt.ElevatorSimulator.Api;

public class SimulatorMovementService : IHostedService
{
    private ISimulator _simulator;
    //private Timer _timer;
    
    public SimulatorMovementService(ISimulator simulator)
    {
        _simulator = simulator;
        //_timer = new Timer()
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}