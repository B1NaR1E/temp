using Dvt.ElevatorSimulator.Domain.Elevator;

namespace Dvt.ElevatorSimulator.Tests.TestImplementations
{
    internal class ElevatorImplementation : Elevator
    {
        public ElevatorImplementation() : base(10, 5)
        {
            
        }

        public ElevatorImplementation(int totalFloors, int totalPassengers) : base(totalFloors, totalPassengers)
        {
        }

        public void SetCurrentFloor(int currentFloor)
        {
            CurrentFloor = currentFloor;
        }

        public void SetDestinationFloor(int destinationFloor)
        {
            DestinationFloor = destinationFloor;
        }
    }
}
