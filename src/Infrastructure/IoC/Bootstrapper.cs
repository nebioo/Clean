using System;
using Autofac;
using Infrastructure.IoC.Modules;

namespace Infrastructure.IoC;

public class Bootstrapper
{
    public static ILifetimeScope Container { get; private set; }

    public static void RegisterModules(ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterModule(new MediatRModule());
        containerBuilder.RegisterModule(new RepositoryModule());
        containerBuilder.RegisterModule(new LoggingModule());
    }

    public static void SetContainer(ILifetimeScope autoFac)
    {
        Container = autoFac;
    }
}

