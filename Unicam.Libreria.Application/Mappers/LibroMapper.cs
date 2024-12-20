using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Libreria.Application.Models.Dtos;
using Unicam.Libreria.Application.Models.Requests;
using Unicam.Libreria.Core.Entities;

namespace Unicam.Libreria.Application.Mappers
{
    public class LibroMapper
    {
        public static Libro ToEntity(AddLibroRequest request)
        {
            var entity = new Libro();
            entity.IdAutore = request.IdAutore;
            entity.IdCategoria = request.IdCategoria;   
            entity.Titolo = request.Titolo;
            entity.Descrizione = request.Descrizione;
            entity.Isbn = request.Isbn;
            return entity;
        }

        public static Libro ToEntity(EditLibroRequest request)
        {
            var entity = new Libro();
            entity.Id = request.Id;
            entity.IdAutore = request.IdAutore;
            entity.IdCategoria = request.IdCategoria;
            entity.Titolo = request.Titolo;
            entity.Descrizione = request.Descrizione;
            entity.Isbn = request.Isbn;
            return entity;
        }

        public static LibroDto ToDto(Libro libro)
        {
            var dto = new LibroDto();
            dto.Descrizione = libro.Descrizione;
            dto.Id = libro.Id;
            dto.IdAutore = libro.IdAutore;
            dto.IdCategoria = libro.IdCategoria;
            dto.Isbn = libro.Isbn;
            dto.Titolo = libro.Titolo;


            return dto;
        }
    }
}
