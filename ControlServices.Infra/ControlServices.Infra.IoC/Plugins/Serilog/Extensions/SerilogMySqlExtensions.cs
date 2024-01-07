using ControlServices.Application.Domain;
using ControlServices.Infra.IoC.Plugins.Serilog.LogEventEnricher;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace ControlServices.Infra.IoC.Plugins.Serilog.Extensions;

public static class SerilogMySqlExtensions
{
    public static void RegisterSerilogMySql(this HostBuilderContext app, IConfigurationBuilder configurationBuilder)
    {
        if (app is null)
        {
            throw new ArgumentNullException(nameof(app));
        }

        if (configurationBuilder is null)
        {
            throw new ArgumentNullException(nameof(configurationBuilder));
        }

        var settings = configurationBuilder.Build();

        var appSettings = new AppSettings();

        settings.Bind(appSettings);

        // Para customizar colunas tem que ir lá no github e customizar mas o código é bastante bossal molezinha
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.With<UserEnricher>()
            .WriteTo.MySQL(
                appSettings.ConnectionStrings.DefaultConnection,
                "AppLogs",
                (LogEventLevel)Enum.Parse(typeof(LogEventLevel), appSettings.Logging.LogLevel.SerilogDb),
                levelSwitch: new LoggingLevelSwitch
                {
                    MinimumLevel = (LogEventLevel)Enum.Parse(typeof(LogEventLevel), appSettings.Logging.LogLevel.SerilogDb)
                })
            .WriteTo.Console()
            .CreateLogger();
    }
}