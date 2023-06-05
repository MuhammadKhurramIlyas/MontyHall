namespace MontyHallSimulatorDataAccess.Interfaces
{
    public interface IMontyHallSimulatorDataAccess
    {
        Task SaveBatchAsync(Guid batchId, int totalSimulations, bool canChangeDoor);
        Task SaveGameAsync(Guid batchId, Guid gameId, bool isWin);
        Task<bool?> IsBatchAggregated(Guid batchId);

        Task<List<MontyHallGameRecord>> GetBatchResults(Guid batchId);
    }
}
