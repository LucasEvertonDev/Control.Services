using Authentication.Application.Domain.Contexts.DbAuth.Clientes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentication.Infra.Data.Contexts.DbAuth.Clientes;
public class ClientesConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasMaxLength(50)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(u => u.Cpf)
            .HasMaxLength(11);

        builder.Property(u => u.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Telefone)
            .HasMaxLength(50);

        builder.Property(u => u.DataNascimento)
            .IsRequired();

        builder.Property(u => u.Situacao)
            .HasConversion<int?>()
            .IsRequired();

        builder.Property(u => u.DataCriacao)
            .HasDefaultValueSql("getdate()")
            .ValueGeneratedOnAdd();

        builder.Property(u => u.DataAtualizacao);
    }
}