using InventoryControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryControl.Infra.Data.EntitiesConfigurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
       
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Nome).HasMaxLength(200).IsRequired();
            builder.Property(u => u.DataNascimento);
            builder.Property(u => u.Telefone);
            builder.Property(u => u.IdExterno);
        }
    }
}
