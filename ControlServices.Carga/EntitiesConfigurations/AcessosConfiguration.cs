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
    public class AcessosConfiguration : IEntityTypeConfiguration<Acesso>
    {
        public void Configure(EntityTypeBuilder<Acesso> builder)
        {
            builder.ToTable("Acesso");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Nome).HasMaxLength(30).IsRequired();
            builder.Property(u => u.Descricao).HasMaxLength(100).IsRequired();
            builder.Property(u => u.Situacao).IsRequired();
        }
    }
}
