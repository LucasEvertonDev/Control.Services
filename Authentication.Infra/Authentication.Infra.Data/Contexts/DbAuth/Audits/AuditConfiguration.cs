using Authentication.Application.Domain.Contexts.DbAuth.Audits;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentication.Infra.Data.Contexts.DbAuth.Audits;

public class AuditConfigurations : IEntityTypeConfiguration<Audit>
{
    public void Configure(EntityTypeBuilder<Audit> builder)
    {
        builder.ToTable("AuditLogs");

        builder.Property(p => p.UserId).HasColumnType("varchar").HasMaxLength(100);
        builder.Property(p => p.Type).HasColumnType("varchar").HasMaxLength(100);
        builder.Property(p => p.TableName).HasColumnType("varchar").HasMaxLength(100);
        builder.Property(p => p.OldValues).HasColumnType("varchar").HasMaxLength(8000);
        builder.Property(p => p.NewValues).HasColumnType("varchar").HasMaxLength(8000);
        builder.Property(p => p.AffectedColumns).HasColumnType("varchar").HasMaxLength(8000);
        builder.Property(p => p.PrimaryKey).HasColumnType("varchar").HasMaxLength(8000);
    }
}