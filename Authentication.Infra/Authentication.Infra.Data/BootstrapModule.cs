using Authentication.Application.Domain;
using Authentication.Infra.Data.Contexts.DbAuth;
using Authentication.Infra.Data.Structure.UnitWork;

namespace Authentication.Infra.Data;

public static class BootstrapModule
{
    public static void RegisterInfraData(this IServiceCollection services, AppSettings configuration)
    {
        if (!configuration.DatabaseInMemory)
        {
            services.AddDbContext<DbAuthContext>(options =>
                options.UseMySql(configuration.ConnectionStrings.DefaultConnection,
                    ServerVersion.AutoDetect(configuration.ConnectionStrings.DefaultConnection))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddScoped<IUnitOfWorkTransaction, UnitOfWork>();
        }
    }
}
