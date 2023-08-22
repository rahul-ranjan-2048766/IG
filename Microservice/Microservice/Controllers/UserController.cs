using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microservice.Models;
using Microservice.Services;

namespace Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;
        public UserController(IUserService service) => this.service = service;

        [AllowAnonymous]
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] User user)
        {
            var result = await service.Add(user);
            if (result == false)
                return BadRequest(new { message = "An error has occurred." });
            return Ok(new { message = "User is registered successfully." });
        }

        [Authorize(Roles = "USER,ADMIN")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await service.Delete(id);
            if (result == false)
                return BadRequest(new { message = "User is not registered." });
            return Ok(new { message = "User is deleted successfully." });
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("DeleteAll")]
        public async Task<IActionResult> DeleteAll()
        {
            _ = await service.DeleteAll();
            return Ok(new { message = "All the users are removed successfully." });
        }

        [Authorize(Roles = "USER,ADMIN")]
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await service.Get(id);
            if (result == null)
                return BadRequest(new { message = "User is not registered." });
            return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await service.GetAll());

        [Authorize(Roles = "USER,ADMIN")]
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            var result = await service.Update(user);
            if (result == false)
                return BadRequest(new { message = "An error has occurred." });
            return Ok(new { message = "User is updated successfully." });
        }
    }
}
