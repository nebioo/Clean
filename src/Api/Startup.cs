using System;
using Api.Attribute;
using ApplicationService.Common;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Infrastructure.IoC;
using Infrastructure.IoC.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Api;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var environmentNamePath = string.IsNullOrEmpty(environmentName) ? "" : environmentName + ".";
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{environmentNamePath}json", false)
            .AddEnvironmentVariables();


        Configuration = builder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        RepositoryModule.AddDbContext(services, Configuration);
        LoggingModule.AddLogging(Configuration);
        services.AddControllers(options => options.Filters.Add(new ValidateModelAttribute(Bootstrapper.Container.Resolve<IAppLogger>())));

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = "Clean.Api",
                    Version = "v1"
                });
        });
    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        var container = app.ApplicationServices.GetAutofacRoot();
        Bootstrapper.SetContainer(container);

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseExceptionHandler("/error");
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clean.Api v1"));

        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
        Bootstrapper.RegisterModules(builder);
    }
}
