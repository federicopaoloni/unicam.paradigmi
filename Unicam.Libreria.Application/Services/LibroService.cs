using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Libreria.Application.Abstractions.Context;
using Unicam.Libreria.Application.Abstractions.Services;
using Unicam.Libreria.Application.Mappers;
using Unicam.Libreria.Application.Models.Requests;
using Unicam.Libreria.Core.Entities;
using Microsoft.EntityFrameworkCore;
namespace Unicam.Libreria.Application.Services
{
    public class LibroService : ILibroService
    {
        private readonly IMyDbContext _context;
        private readonly ILogger _logger;
        public LibroService(ILogger<LibroService> logger, IMyDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<Libro> AddLibroAsync(AddLibroRequest request)
        {
            _logger.LogInformation("Inizio chiamata aggiunta libro");
            var entity = LibroMapper.ToEntity(request);
            await _context.Libri.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteLibroAsync(DeleteLibroRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<Libro> EditLibroAsync(EditLibroRequest request)
        {
            _logger.LogInformation("Inizio chiamata modifica libro");
            var entity = LibroMapper.ToEntity(request);
            var entry = _context.Entry<Libro>(entity);
            entry.Property(x => x.Isbn).IsModified = true;
            entry.Property(x => x.Titolo).IsModified = true;
            entry.Property(x => x.Descrizione).IsModified = true;
            entry.Property(x => x.IdAutore).IsModified = true;
            entry.Property(x => x.IdCategoria).IsModified = true;
            await _context.SaveChangesAsync();
            return entity;
        }

        public Task<List<Libro>> GetLibriAsync()
        {
            throw new NotImplementedException();
        }
    }
}
