using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microservice.Models;
using Microservice.Services;

namespace Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilePicController : ControllerBase
    {
        private readonly IProfilePicService service;
        public ProfilePicController(IProfilePicService service) => this.service = service;

        [Authorize(Roles = "USER")]
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] ProfilePic data)
        {
            var result = await service.Add(data);
            if (result == false)
                return BadRequest(new { message = "An error has occurred." });
            return Ok(new { message = "Your profile pic is saved successfully." });
        }

        [Authorize(Roles = "USER,ADMIN")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _ = await service.Delete(id);
            return Ok(new { message = "Your profile pic is deleted successfully." });
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("DeleteAll")]
        public async Task<IActionResult> DeleteAll()
        {
            _ = await service.DeleteAll();
            return Ok(new { message = "All the pics are deleted successfully." });
        }

        [Authorize(Roles = "USER,ADMIN")]
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id) => Ok(await service.Get(id));

        [Authorize(Roles = "USER,ADMIN")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await service.GetAll());

        [Authorize(Roles = "USER,ADMIN")]
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ProfilePic data)
        {
            var result = await service.Update(data);
            if (result == false)
                return BadRequest(new { message = "An error has occurred." });
            return Ok(new { message = "Your profile pic is updated successfully." });
        }
    }
}
