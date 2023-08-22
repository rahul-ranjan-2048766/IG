using Microservice.Models;
using Microservice.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService service;
        public AdminController(IAdminService service) => this.service = service;

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] Admin admin)
        {
            var result = await service.Add(admin);
            if (result == false)
                return BadRequest(new { message = "An error has occurred." });
            return Ok(new { message = "Admin is registered successfully." });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await service.Delete(id);
            if (result == false)
                return BadRequest(new { message = "Admin is not registered." });
            return Ok(new { message = "Admin is deleted successfully." });
        }

        [HttpDelete("DeleteAll")]
        public async Task<IActionResult> DeleteAll()
        {
            _ = await service.DeleteAll();
            return Ok(new { message = "All the admins are deleted successfully." });
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id) => Ok(await service.Get(id));

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await service.GetAll());

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] Admin admin)
        {
            var result = await service.Update(admin);
            if (result == false)
                return BadRequest(new { message = "An error has occurred." });
            return Ok(new { message = "Admin is updated successfully." });
        }
    }
}
