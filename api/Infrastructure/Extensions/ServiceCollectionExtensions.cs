using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices<TUser>(this IServiceCollection services) where TUser : IdentityUser<int>, new()
        {
            // services.AddTransient<IProfileService, IdentityClaimsProfileService>();
            return services;
        }

        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
            return services;
        }
    }
}