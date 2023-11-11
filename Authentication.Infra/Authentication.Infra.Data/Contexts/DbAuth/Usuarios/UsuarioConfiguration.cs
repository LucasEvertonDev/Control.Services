using Authentication.Application.Domain.Contexts.Usuarios;
using Authentication.Application.Domain.Structure.Enuns;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentication.Infra.Data.Contexts.DbAuth.Usuarios;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");

        // builder.HasQueryFilter(x => x.Situation != (int)Situation.Deleted)
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id).HasMaxLength(50).ValueGeneratedOnAdd().IsRequired();

        builder.Property(u => u.Email).IsRequired().HasMaxLength(50);

        builder.Property(u => u.Senha).HasMaxLength(300).IsRequired();

        builder.Property(u => u.Chave).HasMaxLength(300).IsRequired();

        builder.Property(u => u.Situacao).IsRequired().HasDefaultValue(Situacao.Ativo);

        builder.Property(u => u.DataCriacao).HasDefaultValueSql("getdate()");
    }
}