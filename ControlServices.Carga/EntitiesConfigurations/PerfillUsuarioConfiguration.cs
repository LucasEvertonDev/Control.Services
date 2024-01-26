using InventoryControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Infra.Data.EntitiesConfiguration
{
    public class PerfillUsuarioConfiguration : IEntityTypeConfiguration<PerfilUsuario>
    {
        public void Configure(EntityTypeBuilder<PerfilUsuario> builder)
        {
            builder.ToTable("PerfilUsuario");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Nome).HasMaxLength(30).IsRequired();
            builder.Property(u => u.Situacao).IsRequired();
        }
    }
}
