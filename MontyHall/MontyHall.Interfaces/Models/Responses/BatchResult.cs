namespace MontyHallGame.Interfaces.Models.Responses
{
    public class BatchResult
    {
        public Guid BatchId { get; set; }
        public int? TotalWins { get; set; }
        public int? TotalDefeats { get; set; }
    }
}
