namespace MontyHallGame.Interfaces.Models.Responses
{
    public class GameResult
    {
        public Guid GameId { get; set; }
        public Guid BatchId { get; set; }
        public bool IsWin { get; set; }
    }
}
