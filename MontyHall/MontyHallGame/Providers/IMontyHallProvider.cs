using MontyHallGame.Interfaces.Models.Requests;
using MontyHallGame.Interfaces.Models.Responses;

namespace MontyHallGame.Providers
{
    public interface IMontyHallProvider
    {
        public Task<CreateGameResponse> CreateGameAsync(CreateGameRequest request);
        public Task ChooseDoorAsync(ChooseDoorRequest request);
        public Task<ShowDoorResponse> ShowDoorAsync(ShowDoorRequest request);
        public Task ChangeDoorAsync(ChooseDoorRequest request);
        public Task<GameResult?> GetGameResultAsync(Guid gameId);
        public Task<BatchResult?> GetBatchResultAsync(Guid batchId);
    }
}
