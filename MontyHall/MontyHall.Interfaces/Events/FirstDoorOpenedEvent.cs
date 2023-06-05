namespace MontyHallGame.Interfaces.Events
{
    public class FirstDoorOpenedEvent
    {
        public Guid GameId { get; set; }
        public int FirstDoor { get; set; }
        public bool CanChangeDoor { get; set; }
    }
}
