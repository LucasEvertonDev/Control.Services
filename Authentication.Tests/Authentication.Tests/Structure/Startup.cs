using Authentication.Application.Domain;
using Authentication.Infra.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Method intentionally left empty.
    }
}