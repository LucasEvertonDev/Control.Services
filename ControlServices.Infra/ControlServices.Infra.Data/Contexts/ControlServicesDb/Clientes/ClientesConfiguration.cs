using ControlServices.Application.Domain.Contexts.ControlServicesDb.Clientes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlServices.Infra.Data.Contexts.ControlServicesDb.Clientes;
public class ClientesConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(u => u.Cpf)
            .HasMaxLength(14);

        builder.Property(u => u.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Telefone)
            .HasMaxLength(19);

        builder.Property(u => u.DataNascimento)
            .IsRequired();

        builder.Property(u => u.Situacao)
            .HasConversion<int?>()
            .IsRequired();

        builder.Property(u => u.DataCriacao)
            .HasColumnType("datetime")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(u => u.DataAtualizacao);
    }
}