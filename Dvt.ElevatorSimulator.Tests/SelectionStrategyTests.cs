using Dvt.ElevatorSimulator.Domain.Shared.Enums;
using Dvt.ElevatorSimulator.Infrastructure;
using Dvt.ElevatorSimulator.Infrastructure.Strategies;
using Dvt.ElevatorSimulator.Tests.TestImplementations;

namespace Dvt.ElevatorSimulator.Tests
{
    [TestFixture]
    internal class SelectionStrategyTests
    {
        [Test]
        public void WhenSelectingElevator_IfPassengersAreGoingUp_ItShouldReturn_ClosestElevatorGoingUp()
        {
            //Arrange
            var elevators = ElevatorSetup();
            var selectionStrategy = new ClosestElevatorSelectionStrategy();
            var request = new ElevatorRequest(5, 8, 3);

            //Act
            var elevatorId = selectionStrategy.Run(elevators, request);
            var selectedElevator = elevators.FirstOrDefault(e => e.Id == elevatorId);

            //Assert
            Assert.That(selectedElevator, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(selectedElevator.Direction, Is.EqualTo(Direction.Up));
                Assert.That(selectedElevator.CurrentFloor, Is.EqualTo(4));
                Assert.That(selectedElevator.DestinationFloor, Is.EqualTo(10));
            });
        }

        [Test]
        public void WhenSelectingElevator_IfPassengersAreGoingDown_ItShouldReturn_ClosestElevatorGoingDown()
        {
            //Arrange
            var elevators = ElevatorSetup();
            var selectionStrategy = new ClosestElevatorSelectionStrategy();
            var request = new ElevatorRequest(5, 1, 3);

            //Act
            var elevatorId = selectionStrategy.Run(elevators, request);
            var selectedElevator = elevators.FirstOrDefault(e => e.Id == elevatorId);

            //Assert
            Assert.That(selectedElevator, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(selectedElevator.Direction, Is.EqualTo(Direction.Down));
                Assert.That(selectedElevator.CurrentFloor, Is.EqualTo(6));
                Assert.That(selectedElevator.DestinationFloor, Is.EqualTo(1));
            });
        }

        [Test]
        public void WhenSelectingElevator_IfPassengersAreGoingDown_AndNoElevatorsGoingDown_ItShouldReturn_ClosestStaticElevator()
        {
            //Arrange
            var elevators = ElevatorSetup();
            var selectionStrategy = new ClosestElevatorSelectionStrategy();
            var request = new ElevatorRequest(10, 1, 3);

            //Act
            var elevatorId = selectionStrategy.Run(elevators, request);
            var selectedElevator = elevators.FirstOrDefault(e => e.Id == elevatorId);

            //Assert
            Assert.That(selectedElevator, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(selectedElevator.Direction, Is.EqualTo(Direction.Static));
                Assert.That(selectedElevator.CurrentFloor, Is.EqualTo(1));
                Assert.That(selectedElevator.DestinationFloor, Is.EqualTo(1));
            });
        }

        [Test]
        public void WhenSelectingElevator_IfNoElevatorsAvailable_ItShouldReturnNull()
        {
            //Arrange
            var elevators = ElevatorSetupWithNoAvailableElevators();
            var selectionStrategy = new ClosestElevatorSelectionStrategy();
            var request = new ElevatorRequest(10, 1, 3);

            //Act
            var elevatorId = selectionStrategy.Run(elevators, request);
            var selectedElevator = elevators.FirstOrDefault(e => e.Id == elevatorId);

            //Assert
            Assert.That(selectedElevator, Is.Null);
        }

        private List<ElevatorImplementation> ElevatorSetup()
        {
            var elevators = new List<ElevatorImplementation>();

            var elevator = new ElevatorImplementation();
            elevator.SetCurrentFloor(3);
            elevator.SetDestinationFloor(9);

            elevators.Add(elevator);

            elevator = new ElevatorImplementation();
            elevator.SetCurrentFloor(4);
            elevator.SetDestinationFloor(10);

            elevators.Add(elevator);

            elevator = new ElevatorImplementation();
            elevator.SetCurrentFloor(6);
            elevator.SetDestinationFloor(1);

            elevators.Add(elevator);

            elevator = new ElevatorImplementation();
            elevator.SetCurrentFloor(6);
            elevator.SetDestinationFloor(10);

            elevators.Add(elevator);

            elevator = new ElevatorImplementation();
            elevator.SetCurrentFloor(9);
            elevator.SetDestinationFloor(4);

            elevators.Add(elevator);

            elevators.Add(new ElevatorImplementation());
            elevators.Add(new ElevatorImplementation());

            return elevators;
        }

        private List<ElevatorImplementation> ElevatorSetupWithNoAvailableElevators()
        {
            var elevators = new List<ElevatorImplementation>();

            var elevator = new ElevatorImplementation();
            elevator.SetCurrentFloor(3);
            elevator.SetDestinationFloor(9);

            elevators.Add(elevator);

            elevator = new ElevatorImplementation();
            elevator.SetCurrentFloor(4);
            elevator.SetDestinationFloor(10);

            elevators.Add(elevator);

            elevator = new ElevatorImplementation();
            elevator.SetCurrentFloor(6);
            elevator.SetDestinationFloor(1);

            elevators.Add(elevator);

            elevator = new ElevatorImplementation();
            elevator.SetCurrentFloor(6);
            elevator.SetDestinationFloor(10);

            elevators.Add(elevator);

            elevator = new ElevatorImplementation();
            elevator.SetCurrentFloor(9);
            elevator.SetDestinationFloor(4);

            elevators.Add(elevator);

            return elevators;
        }
    }
}
