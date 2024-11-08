using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicam.Libreria.Core.Entities
{
    public class Autore
    {
        
        public int Id { get; set; }

        
        public string Nome { get; set; } = string.Empty;
        
        public string Cognome { get; set; } = string.Empty;
         
        public virtual ICollection<Libro> Libri { get; set; } = new HashSet<Libro>();
    }

    /* C'è la possibilità di utilizzare le annotazioni per gestire il mapping"
    [Table("Autori")]
    public class Autore
    {
        [Key]
        [Column("IdAutore")]
        public int Id { get; set; }

        [Column("NomeAutore")]
        public string Nome { get; set; } = string.Empty;
        [Column("CognomeAutore")]
        public string Cognome { get; set; } = string.Empty;
    }
    */
}
