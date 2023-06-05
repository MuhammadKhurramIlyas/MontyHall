using MassTransit;
using MontyHallGame.Interfaces.Models.Responses;
using MontyHallGameSimulator.Interfaces.Events;
using MontyHallGameSimulator.Interfaces.Models.Request;
using MontyHallGameSimulator.Interfaces.Models.Response;
using MontyHallSimulatorDataAccess.Interfaces;

namespace MontyHallGameSimulator.Providers
{
    public class SimulationProvider : ISimulationProvider
    {
        private readonly IPublishEndpoint publishEndpoint;
        private readonly HttpClient httpClient;
        private readonly IMontyHallSimulatorDataAccess simulatorDataAccess;

        public SimulationProvider(IPublishEndpoint publishEndpoint, HttpClient httpClient, IMontyHallSimulatorDataAccess simulatorDataAccess)
        {
            this.publishEndpoint = publishEndpoint;
            this.httpClient = httpClient;
            this.simulatorDataAccess = simulatorDataAccess;
        }

        public async Task<Guid> CreateSimulationAsync(CreateSimulationRequest request)
        {
            var sequence = Enumerable.Range(0, request.TotalSimulations).ToList();
            var batchId = Guid.NewGuid();

            await simulatorDataAccess
                .SaveBatchAsync(batchId, request.TotalSimulations, request.CanChangeDoor);

            var createGameTasks = sequence
                .Select(x => CreateGame(batchId, request.CanChangeDoor))
                .ToList();

            await Task.WhenAll(createGameTasks);
            return batchId;
        }

        private async Task CreateGame(Guid batchId, bool canChangeDoor)
        {
            await publishEndpoint.Publish(new CreateGameCommand
            {
                BatchId = batchId,
                CanChangeDoor = canChangeDoor
            });
        }

        public async Task ProcessGameResultsAsync(Guid gameId)
        {
            var uri = $"/api/MontyHall/GetGameResult?GameId={gameId}";
            var result = await httpClient.GetFromJsonAsync<GameResult>(uri);

            if (result == null)
            {
                return;
            }

            await simulatorDataAccess.SaveGameAsync(result.BatchId, result.GameId, result.IsWin);
        }

        public async Task<BatchResult?> ProcessBatchResultsAsync(Guid batchId)
        {
            var isBatchComplete = await simulatorDataAccess.IsBatchAggregated(batchId);

            if (isBatchComplete == null)
            {
                return null;
            }

            if (isBatchComplete == false)
            {
                return new BatchResult
                {
                    BatchId = batchId,
                    IsCompleted = false
                };
            }

            var games = await simulatorDataAccess.GetBatchResults(batchId);

            var totalWins = games.Count(g => g.IsWin);
            var totalDefeats = games.Count - totalWins;

            return new BatchResult
            {
                BatchId = batchId,
                IsCompleted = true,
                TotalDefeats = totalDefeats,
                TotalWins = totalWins
            };
        }
    }
}
