using System.Collections.ObjectModel;
using Authentication.Application.Domain;
using Authentication.Infra.IoC.Plugins.Serilog.LogEventEnricher;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Log = Serilog.Log;

namespace Authentication.Infra.IoC.Plugins.Serilog.Extensions;

public static class SerilogSqlServerExtensions
{
    public static void RegisterSerilog(this HostBuilderContext app, IConfigurationBuilder configurationBuilder)
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

        var levelSwitch = new LoggingLevelSwitch()
        {
            MinimumLevel = (LogEventLevel)Enum.Parse(typeof(LogEventLevel), appSettings.Logging.LogLevel.SerilogDb),
        };

        var options = new ColumnOptions
        {
            AdditionalColumns = new Collection<SqlColumn>
            {
                new SqlColumn { ColumnName = "UserId", DataLength = 50, DataType = System.Data.SqlDbType.NVarChar },
            },
            Message = new ColumnOptions.MessageColumnOptions
            {
                DataLength = 1000
            },
            MessageTemplate = new ColumnOptions.MessageTemplateColumnOptions
            {
                DataLength = 1000
            },
        };

        Log.Logger = new LoggerConfiguration()
          .Enrich.FromLogContext()
          .Enrich.With<UserEnricher>()
          .MinimumLevel.Override("Microsoft", (LogEventLevel)Enum.Parse(typeof(LogEventLevel), appSettings.Logging.LogLevel.MicrosoftAspNetCore))
          .MinimumLevel.Override("System", (LogEventLevel)Enum.Parse(typeof(LogEventLevel), appSettings.Logging.LogLevel.System))
          .WriteTo
             .MSSqlServer(
                 connectionString: appSettings.ConnectionStrings.DefaultConnection,
                 sinkOptions: new MSSqlServerSinkOptions
                 {
                     TableName = "AppLogs",
                     AutoCreateSqlTable = true,
                     AutoCreateSqlDatabase = true,
                     LevelSwitch = levelSwitch,
                 },
                 restrictedToMinimumLevel: (LogEventLevel)Enum.Parse(typeof(LogEventLevel), appSettings.Logging.LogLevel.SerilogDb),
                 formatProvider: null,
                 columnOptions: options,
                 logEventFormatter: null)
            .WriteTo.Console()
         .CreateLogger();
    }
}