using MHW.Companion.Data.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MHW.Companion.Data.Config
{
    public static class ServiceConfig
    {
        public static IServiceCollection RegisterContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>((opt) =>
            opt.UseSqlServer(
                GetConnectionString(configuration), 
                cb => cb.MigrationsHistoryTable("MHWCompanionMigrations")),
            ServiceLifetime.Scoped)
            .AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>());

            return services;
        }

        private static string GetConnectionString(IConfiguration configuration)
        {
            return configuration["data:connectionString"].ToString();
        }
    }
}
