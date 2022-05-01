using Autofac;
using Domain;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module = Autofac.Module;

namespace Infrastructure.IoC.Modules
{
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
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).AsSelf().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).AsSelf().InstancePerLifetimeScope();
            builder.RegisterType(typeof(HttpContextAccessor)).As(typeof(IHttpContextAccessor)).AsSelf().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}

