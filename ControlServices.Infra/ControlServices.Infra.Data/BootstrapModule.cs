using ControlServices.Application.Domain;
using ControlServices.Infra.Data.Contexts.ControlServicesDb;
using ControlServices.Infra.Data.Structure.UnitWork;

namespace ControlServices.Infra.Data;

public static class BootstrapModule
{
    public static void RegisterInfraData(this IServiceCollection services, AppSettings configuration)
    {
        if (!configuration.DatabaseInMemory)
        {
            services.AddDbContext<ControlServicesDbContext>(options =>
                options.UseMySql(configuration.ConnectionStrings.DefaultConnection,
                    ServerVersion.AutoDetect(configuration.ConnectionStrings.DefaultConnection))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddScoped<IUnitOfWorkTransaction, UnitOfWork>();
        }
    }
}
