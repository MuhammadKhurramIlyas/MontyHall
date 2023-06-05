using Microsoft.AspNetCore.Mvc;
using MontyHallGameSimulator.Interfaces.Models.Request;
using MontyHallGameSimulator.Interfaces.Models.Response;
using MontyHallGameSimulator.Providers;

namespace MontyHallGameSimulator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameSimulatorController : ControllerBase
    {
        private readonly ISimulationProvider simulationProvider;
        private readonly ILogger<GameSimulatorController> logger;

        public GameSimulatorController(ISimulationProvider simulationProvider, ILogger<GameSimulatorController> logger)
        {
            this.simulationProvider = simulationProvider;
            this.logger = logger;
        }


        [HttpPost]
        [Route("CreateSimulation")]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type=typeof(Guid))]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateSimulation(CreateSimulationRequest request)
        {
            logger.LogInformation("Simulation creation requested. Total Simulations: {0}, Can Change Door: {1}", request.TotalSimulations, request.CanChangeDoor);
            return Accepted(await simulationProvider.CreateSimulationAsync(request));
        }

        [HttpPost]
        [Route("AggregateGameResult")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Consumes("application/json")]
        public async Task<IActionResult> AggregateGameResult(Guid gameId)
        {
            await simulationProvider.ProcessGameResultsAsync(gameId);
            return Ok();
        }

        [HttpPost]
        [Route("GetBatchResult")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BatchResult))]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetBatchResult(Guid batchId)
        {
            var result = await simulationProvider.ProcessBatchResultsAsync(batchId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
