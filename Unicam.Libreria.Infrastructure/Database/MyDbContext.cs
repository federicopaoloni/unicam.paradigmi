using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Libreria.Application.Abstractions.Context;
using Unicam.Libreria.Core.Entities;
using Unicam.Libreria.Infrastructure.Database.Configurations;

namespace Unicam.Libreria.Infrastructure.Database
{
    public class MyDbContext : DbContext, IMyDbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            
        }
        public virtual DbSet<Autore> Autori { get; set; }
        public virtual DbSet<Libro> Libri { get;set; }
        public virtual DbSet<Categoria> Categorie { get; set; }
        public virtual DbSet<Utente> Utenti { get; set; }
        public virtual DbSet<Ruolo> Ruoli { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfiguration(new AutoreConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            /*
            modelBuilder.Entity<Autore>().HasKey(p=>p.Id);
            modelBuilder.Entity<Autore>().Property(p => p.Id).HasColumnName("IdAutore");
            modelBuilder.Entity<Autore>().Property(p => p.Nome).HasColumnName("NomeAutore");
            modelBuilder.Entity<Autore>().Property(p => p.Cognome).HasColumnName("CognomeAutore");
            */

        }
    }
}
