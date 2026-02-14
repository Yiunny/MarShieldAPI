using MarShield.API.Models;
using MarShield.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarShield.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private readonly InventoryService _service;

        public InventoriesController(InventoryService service) => _service = service;

        [HttpGet("{userId}")] // View user's inventory
        public async Task<ActionResult<Inventory>> Get(string userId)
        {
            var inventory = await _service.GetByUserIdAsync(userId);
            if (inventory is null) return NotFound();
            return inventory;
        }

        [HttpPost("{userId}/add-item")] // Add item to inventory
        public async Task<IActionResult> AddItem(string userId, [FromBody] InventoryItem item)
        {
            await _service.AddItemAsync(userId, item);
            return Ok();
        }
    }
}
