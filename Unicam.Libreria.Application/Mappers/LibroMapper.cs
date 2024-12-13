using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Libreria.Application.Models.Dtos;
using Unicam.Libreria.Core.Entities;

namespace Unicam.Libreria.Application.Mappers
{
    public class LibroMapper
    {

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
