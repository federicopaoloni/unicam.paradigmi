using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
    //[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class LibriController : ControllerBase
    {
        private readonly ILibroService _libroService; 
        public LibriController(ILibroService libroService)
        {
            _libroService = libroService;
        }
        [HttpGet]
        [Route("index")]
        public async Task<IActionResult> Index()
        {
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
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(new { Id = id });
        }

        [HttpPost]
        [Route("add")]
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
