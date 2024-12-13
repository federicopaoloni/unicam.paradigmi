using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Unicam.Libreria.Application.Models.Requests;
using Unicam.Libreria.Core.Entities;

namespace Unicam.Libreria.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class LibriController : ControllerBase
    {

        [HttpGet]
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            return Ok(new { Method = "get" });
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(new { Id = id });
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(AddLibroRequest request)
        {
            return Ok(new { Action = "Add" });
        }
    }
}
