using AutoMapper;
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
        
        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
        {

            services.AddAutoMapper(opt =>
            {
                opt.AddProfile<ViewModelToDataModelMappingProfile>();
                //opt.ForAllMaps((map, exp) => exp.ForAllOtherMembers(mo => mo.Ignore()));
            });
            return services;
        }
    }
}
