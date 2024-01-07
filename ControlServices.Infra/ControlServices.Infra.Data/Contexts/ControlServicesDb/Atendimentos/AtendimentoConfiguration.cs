using ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlServices.Infra.Data.Contexts.ControlServicesDb.Atendimentos;
public class AtendimentoConfiguration : IEntityTypeConfiguration<Atendimento>
{
    public void Configure(EntityTypeBuilder<Atendimento> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(u => u.Data)
            .IsRequired();

        builder.Property(u => u.Situacao)
            .IsRequired();

        builder.Property(u => u.ObservacaoAtendimento)
            .HasMaxLength(200);

        builder.Property(u => u.Situacao)
            .HasConversion<int?>()
            .IsRequired();

        builder.Property(u => u.DataCriacao)
            .HasColumnType("datetime")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(u => u.DataAtualizacao);

        builder.HasOne(u => u.Cliente)
            .WithMany(e => e.Atendimentos)
            .HasForeignKey(a => a.ClienteId);
    }
}