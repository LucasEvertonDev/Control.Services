using InventoryControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryControl.Infra.Data.EntitiesConfigurations
{
    public class ServicoConfiguration : IEntityTypeConfiguration<Servico>
    {
        public void Configure(EntityTypeBuilder<Servico> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Nome).HasMaxLength(30).IsRequired();
            builder.Property(u => u.Descricao).HasMaxLength(200).IsRequired();
            builder.Property(u => u.IdExterno);
        }
    }
}
