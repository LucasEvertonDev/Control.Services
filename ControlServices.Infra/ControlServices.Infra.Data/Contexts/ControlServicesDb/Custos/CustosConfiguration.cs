using ControlServices.Application.Domain.Contexts.ControlServicesDb.Custos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlServices.Infra.Data.Contexts.ControlServicesDb.Custos;
public class CustosConfiguration
{
    public void Configure(EntityTypeBuilder<Custo> builder)
    {
        builder.ToTable("Custos");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(u => u.Data)
            .HasColumnType("datetime")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.Property(u => u.Valor)
            .IsRequired();

        builder.Property(u => u.Descricao)
            .HasMaxLength(100);

        builder.Property(u => u.Situacao)
            .HasConversion<int?>()
            .IsRequired();

        builder.Property(u => u.DataCriacao)
            .HasColumnType("datetime")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(u => u.DataAtualizacao);
    }
}
