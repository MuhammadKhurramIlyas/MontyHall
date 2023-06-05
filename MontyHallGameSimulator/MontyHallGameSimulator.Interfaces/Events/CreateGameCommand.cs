namespace MontyHallGameSimulator.Interfaces.Events
{
    public class CreateGameCommand
    {
        public Guid BatchId { get; set; }
        public bool CanChangeDoor { get; set; }
    }
}
