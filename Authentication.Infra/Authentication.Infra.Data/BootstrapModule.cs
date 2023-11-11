using Authentication.Application.Domain;
using Authentication.Infra.Data.Contexts.DbAuth;
using Authentication.Infra.Data.Structure.UnitWork;

namespace Authentication.Infra.Data;

public static class BootstrapModule
{
    public static void RegisterInfraData(this IServiceCollection services, AppSettings configuration)
    {
        services.AddDbContext<DbAuthContext>(options =>
            options.UseSqlServer(
                configuration.ConnectionStrings.DefaultConnection,
                b => b.MigrationsAssembly(typeof(DbAuthContext).Assembly.FullName)).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        services.AddScoped<IUnitOfWorkTransaction, UnitOfWork>();
    }
}
