using Dvt.ElevatorSimulator.Infrastructure.Interfaces;
using System.Timers;
using Dvt.ElevatorSimulator.Domain.Shared.Enums;
using Timer = System.Timers.Timer;

namespace Dvt.ElevatorSimulator.Api;

public class SimulatorMovementService : IHostedService
{
    private ISimulator _simulator;
    private Timer _timer;

    public SimulatorMovementService(ISimulator simulator)
    {
        _simulator = simulator;
        _timer = new Timer(3000);
        _timer.Elapsed += Timer_Elapsed;
    }

    private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        _simulator.Step();
        DisplayElevatorStatus();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _timer.Start();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _timer.Stop();
    }

    private void DisplayElevatorStatus()
    {
        Console.Clear();
        var elevators = _simulator.GetElevators();
        Console.WriteLine("Elevators");
        Console.WriteLine(
            "-------------------------------------------------------------------------------------------------------------------");
        var elevatorNumber = 1;

        var elevatorNumbers = "";
        var elevatorState = "";
        var elevatorDirection = "";
        var currentFloor = "";
        var totalPassengers = "";
        var destinationFloor = "";

        var numberOfRows = elevators.Count / 5;

        numberOfRows = elevators.Count % 5 > 0 ? numberOfRows + 1 : numberOfRows;

        for (var i = 0; i < numberOfRows; i++)
        {
            var elevatorsToPrint = elevators.Skip(i * 5).Take(5);

            foreach (var elevator in elevatorsToPrint)
            {
                elevatorNumbers += $"No: {elevatorNumber}\t\t\t";
                elevatorState += elevator.State == State.OverLimit
                    ? $"Status: {elevator.State}\t"
                    : $"Status: {elevator.State}\t\t";
                elevatorDirection += elevator.Direction == Direction.Static
                    ? $"Direction: {elevator.Direction}\t"
                    : $"Direction: {elevator.Direction}\t\t";
                currentFloor += $"Current Floor: {elevator.CurrentFloor}\t";
                destinationFloor += $"Destination Floor: {elevator.DestinationFloor}\t";
                totalPassengers += $"Total Passengers: {elevator.TotalPassengers()}\t";
                ++elevatorNumber;
            }

            Console.WriteLine(elevatorNumbers);
            Console.WriteLine(elevatorState);
            Console.WriteLine(elevatorDirection);
            Console.WriteLine(currentFloor);
            Console.WriteLine(destinationFloor);
            Console.WriteLine(totalPassengers);
            Console.WriteLine();

            elevatorNumbers = "";
            elevatorState = "";
            elevatorDirection = "";
            currentFloor = "";
            destinationFloor = "";
            totalPassengers = "";
        }

        Console.WriteLine(
            "-------------------------------------------------------------------------------------------------------------------");
    }
}