using InventoryControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryControl.Infra.Data.EntitiesConfigurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.JsonMessage).IsRequired();
            builder.Property(u => u.TypeMessage).IsRequired();
            builder.Property(u => u.Situacao).IsRequired();
        }
    }
}
