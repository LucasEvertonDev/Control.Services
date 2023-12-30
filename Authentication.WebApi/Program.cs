using Authentication.Infra.IoC.Plugins.Serilog.Extensions;
using Authentication.WebApi.Structure.Startup;
using Serilog;

namespace Authentication.WebApi;

#pragma warning disable S1118 // Utility classes should not have public constructors
public partial class Program
#pragma warning restore S1118 // Utility classes should not have public constructors
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                hostingContext.RegisterSerilogMySql(config);
            })
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.UseIISIntegration();
            });
}
