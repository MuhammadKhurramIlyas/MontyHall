namespace MontyHallGame.Interfaces.Models.Requests
{
    public class CreateGameRequest
    {
        public Guid BatchId { get; set; }
        public bool CanChangeDoor { get; set; }
    }
}
