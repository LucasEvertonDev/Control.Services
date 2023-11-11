using Authentication.Application.Domain;
using Authentication.Infra.IoC;
using Microsoft.Extensions.Configuration;

namespace Authentication.Tests.Structure;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        var appSettings = new AppSettings();

        Configuration.Bind(appSettings);

        services.AddSingleton<AppSettings>(appSettings);

        services.AddInfraStructure(appSettings);
    }
}