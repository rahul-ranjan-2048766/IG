using Microservice.Models;
using Microservice.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService service;
        public ChatController(IChatService service) => this.service = service;

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] Chat chat)
        {
            var result = await service.Add(chat);
            if (result == false)
                return BadRequest(new { message = "An error has occurred." });
            return Ok(new { message = "Chat is saved successfully." });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await service.Delete(id);
            if (result == false)
                return BadRequest(new { message = "An error has occurred." });
            return Ok(new { message = "Chat is deleted successfully." });
        }

        [HttpDelete("DeleteAll")]
        public async Task<IActionResult> DeleteAll()
        {
            _ = await service.DeleteAll();
            return Ok(new { message = "All the chats are deleted successfully." });
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id) => Ok(await service.Get(id));

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await service.GetAll());

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] Chat chat)
        {
            var result = await service.Update(chat);
            if (result == false)
                return BadRequest(new { message = "An error has occurred." });
            return Ok(new { message = "Chat is updated successfully." });
        }
    }
}
