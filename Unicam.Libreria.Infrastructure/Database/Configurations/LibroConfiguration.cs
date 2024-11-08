using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Libreria.Core.Entities;

namespace Unicam.Libreria.Infrastructure.Database.Configurations
{
    public class LibroConfiguration : IEntityTypeConfiguration<Libro>
    {
        public void Configure(EntityTypeBuilder<Libro> builder)
        {
            builder.ToTable("Libri");
            builder.HasKey(x=>x.Id);
            builder.Property(p => p.Id).HasColumnName("IdLibro");

            builder.HasOne(x => x.Autore).WithMany(x=>x.Libri).HasForeignKey(x => x.IdAutore);
            builder.HasOne(x => x.Categoria).WithMany().HasForeignKey(x => x.IdCategoria);
        }
    }
}
