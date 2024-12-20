using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Libreria.Core.Entities;

namespace Unicam.Libreria.Application.Abstractions.Context
{
    public interface IMyDbContext
    {
        public DbSet<Autore> Autori { get; set; }
        public DbSet<Libro> Libri { get; set; }
        public DbSet<Categoria> Categorie { get; set; }
        public DbSet<Ruolo> Ruoli { get; set; }
        public DbSet<Utente> Utenti { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken=default);

        EntityEntry Entry(object entity);
        EntityEntry<T> Entry<T>(T entity) where T:class;
    }
}
