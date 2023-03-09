namespace Dvt.ElevatorSimulator.Tests.Builders
{
    internal static class ElevatorBuilder
    {
        public static void AddElevator(out Guid Id)
        {
            Id = Guid.NewGuid();
        }
    }
}
