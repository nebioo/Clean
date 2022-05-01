using System;
using Autofac;
using Module = Autofac.Module;

namespace Infrastructure.IoC.Modules
{
	public class ApplicationModule : Module
	{
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterAssemblyTypes(System.Reflection.Assembly.Load("Clean.ApplicationService"))
              .Where(t => t.Name.EndsWith("Service"))
              .AsImplementedInterfaces()
              .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(System.Reflection.Assembly.Load("Clean.ApplicationService"))
               .Where(t => t.Name.EndsWith("Configuration"))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}

