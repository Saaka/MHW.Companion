using MHW.Companion.Config;
using Microsoft.Extensions.DependencyInjection;

namespace MHW.Companion.API.Config
{
    public static class ServiceCollectionRegistration
    {
        public static IServiceCollection RegisterWebApiDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAppConfiguration, AppConfiguration>();
            
            return services;
        }
    }
}
