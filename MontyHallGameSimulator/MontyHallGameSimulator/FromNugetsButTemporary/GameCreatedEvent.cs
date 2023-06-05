namespace MontyHallGame.Interfaces.Events
{
    public class GameCreatedEvent
    {
        public Guid GameId { get; set; }
        public bool CanChangeDoor { get; set; }
    }
}
