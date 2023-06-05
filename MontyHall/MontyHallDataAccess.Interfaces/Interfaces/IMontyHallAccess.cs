using MontyHallDataAccess.Interfaces.Models;

namespace MontyHallDataAccess.Interfaces;

public interface IMontyHallAccess
{
    Task<GameRecord?> GetGameAsync(Guid gameId);
    Task<List<GameRecord>?> GetBatchAsync(Guid batchId);
    Task<Guid> AddGameAsync(Guid batchId, bool canChangeDoor);
    Task<GameRecord?> UpdateChoosenDoorAsync(Guid gameId, int playerDoor);
    Task OpenFirstDoorAsync(Guid gameId, int firstDoor);
    Task OpenSecondDoorAsync(Guid gameId, int secondDoor);
}
