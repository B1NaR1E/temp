using Dvt.ElevatorSimulator.Infrastructure.Interfaces;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Dvt.ElevatorSimulator.Api;

public class SimulatorMovementService : IHostedService
{
    private ISimulator _simulator;
    private Timer _timer;
    
    public SimulatorMovementService(ISimulator simulator)
    {
        _simulator = simulator;
        _timer = new Timer(2000);
        _timer.Elapsed += Timer_Elapsed;
    }

    private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        _simulator.Step();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _timer.Start();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _timer.Stop();
    }
}