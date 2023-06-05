namespace MontyHallGameSimulator.Interfaces.Events
{
    public class ChooseDoorCommand
    {
        public Guid GameId { get; set; }
        public int PlayerDoor { get; set; }
    }
}
