using MontyHallGameSimulator.Interfaces.Models.Request;
using MontyHallGameSimulator.Interfaces.Models.Response;

namespace MontyHallGameSimulator.Providers
{
    public interface ISimulationProvider
    {
        Task<Guid> CreateSimulationAsync(CreateSimulationRequest request);
        Task ProcessGameResultsAsync(Guid gameId);
        Task<BatchResult?> ProcessBatchResultsAsync(Guid batchId);
    }
}
