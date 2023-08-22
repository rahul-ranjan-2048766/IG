using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microservice.Models;
using Microservice.Services;

namespace Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService service;
        public AuthenticateController(IAuthenticateService service) => this.service = service;

        [HttpPost("AuthenticateUser")]
        public async Task<IActionResult> AuthenticateUser([FromBody] UserCred userCred)
        {
            (Response?, bool) result = await service.AuthenticateUser(userCred);

            if (result.Item2 == false)
                return BadRequest(new { message = "User is not registered." });

            if (result.Item2 == true && result.Item1 == null)
                return BadRequest(new { message = "Incorrect password." });

            return Ok(result.Item1);
        }

        [HttpPut("AuthenticateAdmin")]
        public async Task<IActionResult> AuthenticateAdmin([FromBody] AdminCred adminCred)
        {
            (AdminResponse?, bool) result = await service.AuthenticateAdmin(adminCred);

            if (result.Item2 == false)
                return BadRequest(new { message = "Admin is not registered." });

            if (result.Item2 == true && result.Item1 == null)
                return BadRequest(new { message = "Incorrect password." });

            return Ok(result.Item1);
        }
    }
}