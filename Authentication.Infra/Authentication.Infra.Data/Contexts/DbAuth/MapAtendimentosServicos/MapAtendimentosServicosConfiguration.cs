using Authentication.Application.Domain.Contexts.DbAuth.MapAtendimentosServicos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentication.Infra.Data.Contexts.DbAuth.MapAtendimentosServicos;
public class MapAtendimentosServicosConfiguration : IEntityTypeConfiguration<MapAtendimentoServico>
{
    public void Configure(EntityTypeBuilder<MapAtendimentoServico> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(u => u.ValorCobrado)
            .IsRequired();

        builder.HasOne(m => m.Atendimento)
            .WithMany(aten => aten.MapAtendimentosServicos)
            .HasForeignKey(m => m.AtendimentoId);

        builder.HasOne(m => m.Servico)
            .WithMany(acesso => acesso.MapAtendimentoServicos)
            .HasForeignKey(m => m.ServicoId);

        builder.Property(u => u.Situacao)
            .HasConversion<int?>()
            .IsRequired();

        builder.Property(u => u.DataCriacao)
            .HasColumnType("datetime")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(u => u.DataAtualizacao);
    }
}
