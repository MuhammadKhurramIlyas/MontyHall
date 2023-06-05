using Microsoft.AspNetCore.Mvc;
using MontyHallGame.Interfaces.Models.Requests;
using MontyHallGame.Providers;

namespace MontyHallGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MontyHallController : ControllerBase
    {
        private readonly IMontyHallProvider montyHallProvider;

        public MontyHallController(IMontyHallProvider montyHallProvider)
        {
            this.montyHallProvider = montyHallProvider;
        }

        [HttpGet]
        [Route("GetGameResult")]
        public async Task<IActionResult> GetGameResult(Guid gameId)
        {
            var result = await montyHallProvider.GetGameResultAsync(gameId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("GetBatchResult")]
        public async Task<IActionResult> GetBatchResult(Guid batchId)
        {
            var result = await montyHallProvider.GetBatchResultAsync(batchId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
