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
    public class FakeLibroService : ILibroService
    {
        public async Task<Libro> AddLibroAsync(AddLibroRequest request)
        {
            return new Libro()
            {
                Id = 1,
                Titolo = request.Titolo,
                Isbn = request.Isbn,
                Descrizione = request.Descrizione,
                IdAutore = request.IdAutore,
                IdCategoria = request.IdCategoria
            };
        }

        public async Task DeleteLibroAsync(DeleteLibroRequest request)
        {
            
        }

        public async Task<Libro> EditLibroAsync(EditLibroRequest request)
        {
            return new Libro()
            {
                Id = request.Id,
                Titolo = request.Titolo,
                Isbn = request.Isbn,
                Descrizione = request.Descrizione,
                IdAutore = request.IdAutore,
                IdCategoria = request.IdCategoria
            };
        }

        public async Task<List<Libro>> GetLibriAsync()
        {
            var list = new List<Libro>();
            for(int i = 1; i < 5; i++)
            {
                list.Add(new Libro()
                {
                    Id = i,
                    Titolo = $"Libro {i}",
                    Descrizione = $"Descrizione {i}",
                    IdAutore = 1,
                    IdCategoria = 1,
                    Isbn = $"ISBN {i}"
                });
            }

            return list;
        }
    }
}
