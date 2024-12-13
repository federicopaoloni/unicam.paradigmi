using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Libreria.Application.Abstractions.Services;
using Unicam.Libreria.Application.Models.Requests;
using Unicam.Libreria.Core.Entities;

namespace Unicam.Libreria.Application.Services
{
    public class LibroService : ILibroService
    {
        public async Task<Libro> AddLibroAsync(AddLibroRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteLibroAsync(DeleteLibroRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<Libro> EditLibroAsync(EditLibroRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<List<Libro>> GetLibriAsync()
        {
            throw new NotImplementedException();
        }
    }
}
