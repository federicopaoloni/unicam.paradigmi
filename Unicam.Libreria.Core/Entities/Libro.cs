using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicam.Libreria.Core.Entities
{
    public class Libro
    {
        public int Id { get; set; }
        public string Isbn { get; set; } = string.Empty;
        public string Titolo { get; set; } = string.Empty;
        public string Descrizione { get; set; } = string.Empty;

        public int IdAutore { get; set; }

        public int IdCategoria { get; set; }
        public virtual Autore Autore { get; set; } = null!;
        public virtual Categoria Categoria { get; set; } = null!;
        
    }
}
