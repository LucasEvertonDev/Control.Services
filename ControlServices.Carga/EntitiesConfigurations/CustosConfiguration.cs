using InventoryControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryControl.Infra.Data.EntitiesConfigurations
{
    public class CustosConfiguration : IEntityTypeConfiguration<Custo>
    {
        public void Configure(EntityTypeBuilder<Custo> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Data).IsRequired();
            builder.Property(u => u.Valor).IsRequired();
            builder.Property(u => u.Descricao).HasMaxLength(200).IsRequired();
        }
    }
}
