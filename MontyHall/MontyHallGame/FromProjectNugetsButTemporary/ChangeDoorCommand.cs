namespace MontyHallGameSimulator.Interfaces.Events
{
    public class ChangeDoorCommand
    {
        public Guid GameId { get; set; }
        public int PlayerDoor { get; set; }
    }
}
