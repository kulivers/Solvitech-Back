using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Entities.Shared;
using Solvintech.Services.Contracts;

namespace PasswordManager.Presentation.Controllers
{
    [ApiController, Produces("application/json"), Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IServiceManager _service;
        public RegistrationController(IServiceManager service) => _service = service;

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto user)
        {
            await _service.AuthenticationService.RegisterUser(user);
            return Ok();
        }
    }
}