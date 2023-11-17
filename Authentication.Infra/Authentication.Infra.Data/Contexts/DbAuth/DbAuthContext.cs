using Authentication.Application.Domain.Contexts.DbAuth.Usuarios;

namespace Authentication.Infra.Data.Contexts.DbAuth;

public class DbAuthContext(DbContextOptions<DbAuthContext> options) : DbContext(options)
{
    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply configurations automatic
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbAuthContext).Assembly);
    }
}
