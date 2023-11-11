using Authentication.Infra.IoC.Plugins.Serilog.Extensions;
using Authentication.WebApi.Structure.Startup;
using Serilog;

namespace Authentication.WebApi;

public static partial class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                hostingContext.RegisterSerilog(config);
            })
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.UseIISIntegration();
            });
}
