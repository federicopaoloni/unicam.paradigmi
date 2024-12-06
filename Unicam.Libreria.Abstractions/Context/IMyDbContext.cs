using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicam.Libreria.Abstractions.Context
{
    public interface IMyDbContext
    {
        public DbSet<Autore> Autori { get; set; }
        public DbSet<Libro> Libri { get; set; }
        public DbSet<Categoria> Categorie { get; set; }
    }
}
