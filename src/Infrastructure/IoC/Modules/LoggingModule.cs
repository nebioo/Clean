using System;
using ApplicationService.Common;
using Autofac;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Formatting.Json;
using Module = Autofac.Module;

namespace Infrastructure.IoC.Modules;

public class LoggingModule : Module
{
    private static string _environmentName;
    private static string _loggerAppName;

    public static void AddLogging(IConfiguration configuration)
    {
        _loggerAppName = configuration["Logging.ApplicationName"];
        _environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.Register<ILogger>((c, p) => new LoggerConfiguration()
            .Enrich.WithMachineName()
            .WriteTo.Console(new JsonFormatter())
            .Enrich.WithProperty("Environment", _environmentName)
            .Enrich.WithProperty("MachineName", Environment.MachineName)
            .Enrich.WithProperty("LoggerApplicationName", _loggerAppName)
            .CreateLogger()).SingleInstance();

        builder.RegisterType<AppLogger>().As<IAppLogger>().InstancePerLifetimeScope();

        base.Load(builder);
    }
}