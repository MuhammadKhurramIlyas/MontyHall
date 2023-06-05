namespace MontyHallSimulatorDataAccess.Interfaces
{
    public class MontyHallGameRecord
    {
        public Guid BatchId { get; set; }
        public Guid GameId { get; set; }
        public bool IsWin { get; set; }
    }
}
