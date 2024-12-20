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
    public class RuoloConfiguration : IEntityTypeConfiguration<Ruolo>
    {
        public void Configure(EntityTypeBuilder<Ruolo> builder)
        {
            builder.ToTable("Ruoli");
            builder.HasKey(x => x.IdRuolo);
        }
    }
}
