using MHW.Companion.Data.Store;
using Microsoft.Extensions.DependencyInjection;

namespace MHW.Companion.Data.Config
{
    public static class ServiceConfig
    {
        public static IServiceCollection RegisterContext(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(ServiceLifetime.Scoped);

            return services;
        }
    }
}
