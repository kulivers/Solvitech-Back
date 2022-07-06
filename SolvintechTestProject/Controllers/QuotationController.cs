using System.Linq;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Solvintech.Services.Contracts;

namespace PasswordManager.Presentation.Controllers
{
    [ApiController, Produces("application/json"), Route("api/[controller]")]
    public class QuotationController : ControllerBase
    {
        private readonly IServiceManager _service;
        public QuotationController(IServiceManager service) => _service = service;


        [HttpGet("{day}-{month}-{year}")]
        [Authorize]
        public async Task<IActionResult> GetJsonQuotations(string day, string month, string year)
        {
            var date = new StringDate(day, month, year);
            var valutes = await _service.QuotationService.GetJsonQuotatiation(date);
            if (valutes!=null) return Ok(valutes);
            return NotFound();
        }
    }
}