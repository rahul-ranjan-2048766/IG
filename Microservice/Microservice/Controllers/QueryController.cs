using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microservice.Models;
using Microservice.Services;

namespace Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        private readonly IQueryService service;
        public QueryController(IQueryService service) => this.service = service;

        [AllowAnonymous]
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] Query query)
        {
            var result = await service.Add(query);
            if (result == false)
                return BadRequest(new { message = "An error has occurred." });
            return Ok(new { message = "Congrats, Your query is sent successsfully." });
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await service.Delete(id);
            if (result == false)
                return BadRequest(new { message = "Query is not available." });
            return Ok(new { message = "Query is deleted successfully." });
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("DeleteAll")]
        public async Task<IActionResult> DeleteAll()
        {
            _ = await service.DeleteAll();
            return Ok(new { message = "All the queries are deleted successfully." });
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await service.Get(id);
            if (result == null)
                return BadRequest(new { message = "Query is not available." });
            return Ok(result);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await service.GetAll());

        [Authorize(Roles = "NOBODY")]
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] Query query)
        {
            var result = await service.Update(query);
            if (result == false)
                return BadRequest(new { message = "Query is not available." });
            return Ok(new { message = "Query is updated successfully." });
        }
    }
}
