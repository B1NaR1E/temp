using Dvt.ElevatorSimulator.Domain.Shared.Enums;
using Dvt.ElevatorSimulator.Infrastructure;
using Dvt.ElevatorSimulator.Tests.TestImplementations;

namespace Dvt.ElevatorSimulator.Tests
{
    [TestFixture]
    public class ElevatorTests
    {
        public class DescribeLoading
        {
            [Test]
            public void WhenLoadingPassengers_WhenLimitIsExceeded_ItShouldSetStateToOverLimit()
            {
                //Arrange
                var elevator = new ElevatorImplementation(totalFloors: 10, totalPassengers: 5);
                var requests = new List<ElevatorRequest>()
            {
                new(5, 10, 3),
                new(5, 9, 3),
            };

                //Act
                requests.ForEach(request =>
                {
                    elevator.LoadPassenger(request.DestinationFloor, request.OriginatingFloor, request.TotalPassengers);
                });

                //Assert
                Assert.Multiple(() =>
                {
                    Assert.That(elevator.State, Is.EqualTo(State.OverLimit));
                    Assert.That(elevator.TotalPassengers(), Is.EqualTo(6));
                    Assert.That(elevator.Stops, Has.Count.EqualTo(1));
                });
            }

            [Test]
            public void WhenLoadingPassengers_WhenLimitIsNotExceeded_ItShouldLoadPassengers_AndSetDestinations()
            {
                //Arrange
                var elevator = new ElevatorImplementation(totalFloors: 10, totalPassengers: 5);
                var requests = new List<ElevatorRequest>()
            {
                new(5, 10, 3),
                new(5, 9, 2),
            };

                //Act
                requests.ForEach(request =>
                {
                    elevator.LoadPassenger(request.DestinationFloor, request.OriginatingFloor, request.TotalPassengers);
                });

                //Assert
                Assert.Multiple(() =>
                {
                    Assert.That(elevator.State, Is.EqualTo(State.Stopped));
                    Assert.That(elevator.TotalPassengers(), Is.EqualTo(5));
                    Assert.That(elevator.Stops, Has.Count.EqualTo(2));
                });
            }
        }

        public class DescribeUnloading
        {
            [Test]
            public void WhenUnloadingPassengers_IfCurrentFloorEqualsDestinationFloor_ItShouldUnloadPassengers()
            {
                //Arrange
                var elevator = new ElevatorImplementation(totalFloors: 10, totalPassengers: 5);
                elevator.SetCurrentFloor(9);

                var requests = new List<ElevatorRequest>()
            {
                new(5, 10, 4),
            };

                //Act
                requests.ForEach(request =>
                {
                    elevator.LoadPassenger(request.DestinationFloor, request.OriginatingFloor, request.TotalPassengers);
                });

                elevator.Move();

                var unloadedPassengers = elevator.UnloadPassengers();

                //Assert
                Assert.Multiple(() =>
                {
                    Assert.That(elevator.State, Is.EqualTo(State.Stopped));
                    Assert.That(elevator.TotalPassengers(), Is.EqualTo(0));
                    Assert.That(elevator.Stops, Has.Count.EqualTo(0));
                    Assert.That(unloadedPassengers.ToList(), Has.Count.EqualTo(4));
                });
            }


            [Test]
            public void WhenUnloadingPassengers_WhenLimitIsExceeded_ItShouldUnloadPassengersThatExceededLimit()
            {
                //Arrange
                var elevator = new ElevatorImplementation(totalFloors: 10, totalPassengers: 5);
                elevator.SetCurrentFloor(6);

                var requests = new List<ElevatorRequest>()
            {
                new(5, 10, 4),
                new(6, 9, 2),
            };

                //Act
                requests.ForEach(request =>
                {
                    elevator.LoadPassenger(request.DestinationFloor, request.OriginatingFloor, request.TotalPassengers);
                });

                var unloadedPassengers = elevator.UnloadPassengers();

                //Assert
                Assert.Multiple(() =>
                {
                    Assert.That(elevator.State, Is.EqualTo(State.Stopped));
                    Assert.That(elevator.TotalPassengers(), Is.EqualTo(4));
                    Assert.That(elevator.Stops, Has.Count.EqualTo(1));
                    Assert.That(unloadedPassengers.ToList(), Has.Count.EqualTo(2));
                });
            }
        }

        public class DescribeMoving
        {
            [Test]
            public void WhenMoving_IfDestinationFloorIsGreaterThanCurrentFloor_ItShouldMoveUp()
            {
                //Arrange
                var elevator = new ElevatorImplementation(totalFloors: 10, totalPassengers: 5);
                elevator.SetCurrentFloor(5);

                var request = new ElevatorRequest(5, 10, 3);

                //Act
                elevator.LoadPassenger(request.DestinationFloor, request.OriginatingFloor, request.TotalPassengers);

                elevator.Move();

                //Assert
                Assert.Multiple(() =>
                {
                    Assert.That(elevator.State, Is.EqualTo(State.Moving));
                    Assert.That(elevator.CurrentFloor, Is.EqualTo(request.OriginatingFloor + 1));
                    Assert.That(elevator.DestinationFloor, Is.EqualTo(request.DestinationFloor));
                    Assert.That(elevator.Direction, Is.EqualTo(Direction.Up));
                    Assert.That(elevator.TotalPassengers(), Is.EqualTo(3));
                    Assert.That(elevator.Stops, Has.Count.EqualTo(1));
                });
            }

            [Test]
            public void WhenMoving_IfDestinationFloorIsSmallerThanCurrentFloor_ItShouldMoveDown()
            {
                //Arrange
                var elevator = new ElevatorImplementation(totalFloors: 10, totalPassengers: 5);
                elevator.SetCurrentFloor(5);

                var request = new ElevatorRequest(5, 1, 3);

                //Act
                elevator.LoadPassenger(request.DestinationFloor, request.OriginatingFloor, request.TotalPassengers);

                elevator.Move();

                //Assert
                Assert.Multiple(() =>
                {
                    Assert.That(elevator.State, Is.EqualTo(State.Moving));
                    Assert.That(elevator.CurrentFloor, Is.EqualTo(request.OriginatingFloor - 1));
                    Assert.That(elevator.DestinationFloor, Is.EqualTo(request.DestinationFloor));
                    Assert.That(elevator.Direction, Is.EqualTo(Direction.Down));
                    Assert.That(elevator.TotalPassengers(), Is.EqualTo(3));
                    Assert.That(elevator.Stops, Has.Count.EqualTo(1));
                });
            }

            [Test]
            public void WhenMoving_IfDestinationFloorIsEqualToCurrentFloor_ItShouldStop()
            {
                //Arrange
                var elevator = new ElevatorImplementation(totalFloors: 10, totalPassengers: 5);
                elevator.SetCurrentFloor(5);

                var request = new ElevatorRequest(5, 10, 3);

                //Act
                elevator.LoadPassenger(request.DestinationFloor, request.OriginatingFloor, request.TotalPassengers);

                do
                {
                    elevator.Move();
                }
                while (elevator.CurrentFloor != elevator.DestinationFloor);

                //Assert
                Assert.Multiple(() =>
                {
                    Assert.That(elevator.State, Is.EqualTo(State.Stopped));
                    Assert.That(elevator.CurrentFloor, Is.EqualTo(request.DestinationFloor));
                    Assert.That(elevator.Direction, Is.EqualTo(Direction.Static));
                    Assert.That(elevator.TotalPassengers(), Is.EqualTo(3));
                    Assert.That(elevator.Stops, Has.Count.EqualTo(0));
                });
            }
        }
    }
}