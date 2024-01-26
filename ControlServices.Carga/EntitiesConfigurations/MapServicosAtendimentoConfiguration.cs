using InventoryControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryControl.Infra.Data.EntitiesConfigurations
{
    public class MapServicosAtendimentoConfiguration : IEntityTypeConfiguration<MapServicosAtendimento>
    {
        public void Configure(EntityTypeBuilder<MapServicosAtendimento> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.ValorCobrado).IsRequired();
            builder.Property(u => u.IdExterno);
            builder.Property(u => u.ServicoIdExterno);
            builder.Property(u => u.AtendimentoIdExterno);
            builder.HasOne(m => m.Atendimento)
                .WithMany(aten => aten.MapServicosAtendimentos)
                .HasForeignKey(m => m.AtendimentoId);
            builder.HasOne(m => m.Servico)
                .WithMany(acesso => acesso.MapServicosAtendimentos)
                .HasForeignKey(m => m.ServicoId);
        }
    }
}
