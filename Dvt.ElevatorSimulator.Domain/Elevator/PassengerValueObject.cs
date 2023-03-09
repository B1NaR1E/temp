namespace Dvt.ElevatorSimulator.Domain.Elevator
{
    public class PassengerValueObject
    {
        public int DestinationFloor { get; }
        public int CurrentFloor { get; }

        public PassengerValueObject(int destinationFloor, int currentFloor)
        {
            DestinationFloor = destinationFloor;
            CurrentFloor = currentFloor;
        }
    }
}
