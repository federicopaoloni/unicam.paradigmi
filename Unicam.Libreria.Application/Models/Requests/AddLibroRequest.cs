using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicam.Libreria.Application.Models.Requests
{
    public class AddLibroRequest
    {
        public string Isbn { get; set; } = string.Empty;
        
        public string Titolo { get; set; } = string.Empty;
        public string Descrizione { get; set; } = string.Empty;
        [Range(0,10)]
        public int IdAutore { get; set; }
        public int IdCategoria { get; set; }
    }
}
