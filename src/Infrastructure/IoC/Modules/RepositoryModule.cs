using System.Reflection;
using Autofac;
using Infrastructure.Persistence;
using Infrastructure.RepositoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module = Autofac.Module;

namespace Infrastructure.IoC.Modules;

public class RepositoryModule : Module
	{
    private static string _connectionString;
    public static void AddDbContext(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        _connectionString = configuration["DbConnString"];
        serviceCollection.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(_connectionString));
    }

    protected override void Load(ContainerBuilder builder)
    {
        var assemblyType = typeof(TodoRepository).GetTypeInfo();

        builder.RegisterAssemblyTypes(assemblyType.Assembly)
            .Where(x => x != typeof(ApplicationDbContext))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        base.Load(builder);
    }
}

