using Authentication.Application.Domain.Contexts.Usuarios;
using Authentication.Infra.Data.Structure;

namespace Authentication.Infra.Data.Contexts.DbAuth;

public class DbAuthContext(DbContextOptions<DbAuthContext> options) : DbContext(options)
{
    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Audit> Audits { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await BeforeSaveChanges();
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply configurations automatic
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbAuthContext).Assembly);
    }

    private async Task BeforeSaveChanges()
    {
        try
        {
            var login = "lcseverton@gmail.com";
            ChangeTracker.DetectChanges();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is IEntity auditable)
                {
                    // entry.State
                    auditable.UpdateDate();
                }

                if (entry.Entity is Audit || entry.State is EntityState.Detached or EntityState.Unchanged)
                {
                    continue;
                }

                var auditEntry = new AuditEntry(entry) { TableName = entry.Entity.GetType().Name, UserId = login };

                foreach (var property in entry.Properties)
                {
                    var propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if (!object.Equals(property.OriginalValue, property.CurrentValue))
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }

                            break;
                    }
                }

                await Audits.AddAsync(auditEntry.ToAudit());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
