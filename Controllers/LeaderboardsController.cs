using MarShield.API.Models;
using MarShield.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarShield.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardsController : ControllerBase
    {
        private readonly LeaderboardService _service;

        public LeaderboardsController(LeaderboardService service) => _service = service;

        [HttpGet] // Get top players
        public async Task<List<Leaderboard>> GetTop() => await _service.GetTopPlayersAsync();

        [HttpPost] // Update score
        public async Task<IActionResult> PostScore([FromBody] Leaderboard entry)
        {
            await _service.CreateOrUpdateAsync(entry);
            return Ok();
        }
    }
}
