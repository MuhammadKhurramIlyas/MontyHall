namespace MontyHallGameSimulator.Interfaces.Models.Response
{
    public class BatchResult
    {
        public Guid BatchId { get; set; }
        public bool IsCompleted { get; set; }
        public int? TotalWins { get; set; }
        public int? TotalDefeats { get; set; }
    }
}
