using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Entities.Shared;
using Solvintech.Services.Contracts;

namespace PasswordManager.Presentation.Controllers
{
    [ApiController, Produces("application/json"), Route("api/[controller]")]
    public class QuotationController : ControllerBase

    {
        

        [HttpGet("{day}-{month}-{year}")]
        [Authorize]
        public async Task<IActionResult> GetJsonQuotations(int day, int month, int year)
        {
            return Ok(new { day = day, month = month, year = year });
        }
    }
}