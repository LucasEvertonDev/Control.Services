
using InventoryControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryControl.Infra.Data.EntitiesConfigurations
{
    public class AtendimentoConfiguration : IEntityTypeConfiguration<Atendimento>
    {
        public void Configure(EntityTypeBuilder<Atendimento> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Data).IsRequired();
            builder.Property(u => u.Situacao).IsRequired();
            builder.Property(u => u.ObservacaoAtendimento).HasMaxLength(200);
            builder.Property(u => u.IdExterno);
            builder.Property(u => u.ClienteIdExterno);
        }
    }
}
