using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Libreria.Application.Models.Requests;
using Unicam.Libreria.Core.Entities;

namespace Unicam.Libreria.Application.Abstractions.Services
{
    public interface ILibroService
    {
        Task<List<Libro>> GetLibriAsync();
        Task<Libro> AddLibroAsync(AddLibroRequest request);

        Task<Libro> EditLibroAsync(EditLibroRequest request);

        Task DeleteLibroAsync(DeleteLibroRequest request);
        
    }
}
