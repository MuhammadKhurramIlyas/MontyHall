using Microsoft.EntityFrameworkCore;
using MontyHallSimulatorDataAccess.Database;
using MontyHallSimulatorDataAccess.Interfaces;

namespace MontyHallSimulatorDataAccess
{
    public class MontyHallSimulatorDataAccess : IMontyHallSimulatorDataAccess
    {
        private readonly MontyHallSimulatorDataContext context;

        public MontyHallSimulatorDataAccess(MontyHallSimulatorDataContext context)
        {
            this.context = context;
        }

        public async Task SaveBatchAsync(Guid batchId, int totalSimulations, bool canChangeDoor)
        {
            context.EnableChanges();

            context.montyHallBatches.Add(new Database.Entities.MontyHallBatch
            {
                BatchId = batchId,
                CanChangeDoor = canChangeDoor,
                TotalSimulations = totalSimulations
            });

            await context.SaveChangesAsync();
        }

        public async Task SaveGameAsync(Guid batchId, Guid gameId, bool isWin)
        {
            context.EnableChanges();

            context.montyHallGames.Add(new Database.Entities.MontyHallGame
            {
                BatchId = batchId,
                GameId = gameId,
                IsWin = isWin
            });

            await context.SaveChangesAsync();
        }

        public async Task<bool?> IsBatchAggregated(Guid batchId)
        {
            var batch = await context.montyHallBatches
                .Include(x => x.MontyHallGames)
                .SingleOrDefaultAsync(x => x.BatchId == batchId);

            if (batch == null)
            {
                return null;
            }

            return batch.TotalSimulations == (batch.MontyHallGames?.Count ?? 0);
        }

        public async Task<List<MontyHallGameRecord>> GetBatchResults(Guid batchId)
        {
            return await context.montyHallGames.Where(x => x.BatchId == batchId).Select(x => new MontyHallGameRecord
            {
                BatchId = batchId,
                GameId = x.GameId,
                IsWin = x.IsWin
            }).ToListAsync();
        }
    }
}
