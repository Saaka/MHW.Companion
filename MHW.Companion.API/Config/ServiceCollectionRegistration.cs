using AutoMapper;
using MHW.Companion.Config;
using MHW.Companion.Data.Store;
using MHW.Companion.Model.User;
using MHW.Companion.ViewModels.Mappings;
using Microsoft.AspNetCore.Identity;
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

        public static IServiceCollection RegisterIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<AppUser>(o =>
            {
                o.Password.RequireNonAlphanumeric = false;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole<int>), builder.Services);
            builder.AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            return services;
        }
    }
}
