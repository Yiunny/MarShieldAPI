using MarShield.API.Models;
using MarShield.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarShield.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameSessionsController : ControllerBase
    {
        private readonly GameSessionService _service;

        public GameSessionsController(GameSessionService service) => _service = service;

        [HttpPost("start")] //Start playing a new game session
        public async Task<IActionResult> StartSession([FromBody] GameSession session)
        {
            await _service.CreateAsync(session);
            return Ok(session);
        }

        [HttpPut("{id}")] // Sync data (Heartbeat sync)
        public async Task<IActionResult> UpdateSession(string id, [FromBody] GameSession session)
        {
            var existingSession = await _service.GetAsync(id);
            if (existingSession is null) return NotFound();

            session.Id = existingSession.Id; // Keep the same ID
            await _service.UpdateSyncAsync(id, session);
            return NoContent();
        }
    }
}