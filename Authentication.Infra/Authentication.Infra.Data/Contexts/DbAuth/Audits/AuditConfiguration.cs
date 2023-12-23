using Authentication.Application.Domain.Contexts.DbAuth.Audits;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentication.Infra.Data.Contexts.DbAuth.Audits;

public class AuditConfigurations : IEntityTypeConfiguration<Audit>
{
    public void Configure(EntityTypeBuilder<Audit> builder)
    {
        builder.ToTable("AuditLogs");

        builder.Property(p => p.UserId).HasMaxLength(100);
        builder.Property(p => p.Type).HasMaxLength(100);
        builder.Property(p => p.TableName).HasMaxLength(100);
        builder.Property(p => p.OldValues).HasMaxLength(8000);
        builder.Property(p => p.NewValues).HasMaxLength(8000);
        builder.Property(p => p.AffectedColumns).HasMaxLength(8000);
        builder.Property(p => p.PrimaryKey).HasMaxLength(8000);
    }
}