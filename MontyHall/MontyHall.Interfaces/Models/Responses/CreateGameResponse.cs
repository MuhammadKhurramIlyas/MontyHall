namespace MontyHallGame.Interfaces.Models.Responses
{
    public class CreateGameResponse
    {
        public Guid GameId { get; set; }
        public bool CanChangeDoor { get; set; }
    }
}
