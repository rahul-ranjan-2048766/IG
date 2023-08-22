using Microservice.Models;
using Microservice.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityController : ControllerBase
    {
        private readonly IMessageService service;
        public CommunityController(IMessageService service) => this.service = service;

        [Authorize]
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] Community data)
        {
            var result = await service.Add(data);
            if (result == false)
                return BadRequest(new { message = "An error has occurred." });
            return Ok(new { message = "Message is saved successfully." });
        }

        [Authorize]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await service.Delete(id);
            if (result == false)
                return BadRequest(new { message = "Message is not present." });
            return Ok(new { message = "Message is deleted successfully." });
        }

        [Authorize]
        [HttpDelete("DeleteAll")]
        public async Task<IActionResult> DeleteAll()
        {
            _ = await service.DeleteAll();
            return Ok(new { message = "All the messages are deleted successfully." });
        }

        [Authorize]
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id) =>
            Ok(await service.Get(id));

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() =>
            Ok(await service.GetAll());

        [Authorize]
        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] Community data)
        {
            var result = await service.Update(data);
            if (result == false)
                return BadRequest(new { message = "Message is not present." });
            return Ok(new { message = "Message is updated successfully." });
        }
    }
}
