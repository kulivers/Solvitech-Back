using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Entities.Shared;
using Solvintech.Services.Contracts;

namespace PasswordManager.Presentation.Controllers
{
    [ApiController, Produces("application/json"), Route("api/[controller]")]
    public class TokensController : ControllerBase
    {
        private readonly IServiceManager _service;

        public TokensController(IServiceManager service)
        {
            _service = service;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GetToken([FromBody] UserForAuthenticationDto user)
        {
            var tokenDto = await _service.AuthenticationService.GetUserTokenAsync(user);
            if (tokenDto == null)
                return Unauthorized();
            return Ok(new { token = tokenDto });
        }

        [HttpPost("generate")] //todo move to own controller
        [Authorize]
        public async Task<IActionResult> GenerateNewToken([FromBody] UserForAuthenticationDto user) =>
            Ok(await _service.AuthenticationService.GenerateNewUserToken(user));
    }
}