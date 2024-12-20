using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Unicam.Libreria.Application.Abstractions.Services;
using Unicam.Libreria.Application.Factories;
using Unicam.Libreria.Application.Mappers;
using Unicam.Libreria.Application.Models.Dtos;
using Unicam.Libreria.Application.Models.Requests;
using Unicam.Libreria.Application.Models.Responses;
using Unicam.Libreria.Core.Entities;

namespace Unicam.Libreria.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class LibriController : ControllerBase
    {
        private readonly ILibroService _libroService;
        private readonly ILogger _logger;
        public LibriController(ILibroService libroService, ILogger<LibriController> logger)
        {
            _libroService = libroService;
            _logger = logger;
        }
        [HttpGet]
        [Route("index")]
        [ProducesResponseType<BaseResponse<List<LibroDto>>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<BaseResponse<string>>(StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("OK dovrebbe andare nei log");
            _logger.LogDebug("Questa non ci dovrebbe andare");
            var result= await _libroService.GetLibriAsync();
            return Ok(
               ResponseFactory
               .WithSuccess(
                   result.Select(s=>
                   LibroMapper.ToDto(s)
                   ).ToList()
                   )
            );
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(new { Id = id });
        }

        [HttpPost]
        [Route("add")]
        [Authorize(policy:"IS_ADM")]
        public async Task<IActionResult> Add(AddLibroRequest request)
        {
            var result = await _libroService.AddLibroAsync(request);
            return Ok(
                ResponseFactory
                .WithSuccess(LibroMapper.ToDto(result))
             );
        }

        [HttpPost]
        [Route("edit")]
        [Authorize(Roles = "ADMIN", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Edit(EditLibroRequest request)
        {
            var result = await _libroService.EditLibroAsync(request);
            return Ok(
                ResponseFactory
                .WithSuccess(LibroMapper.ToDto(result))
             );
        }
    }
}
