using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MHW.Companion.Data.Config;
using MHW.Companion.API.Config;
using AutoMapper;
using MHW.Companion.Model.User;
using MHW.Companion.Data.Store;
using Microsoft.AspNetCore.Identity;
using MHW.Companion.ViewModels.Mappings;

namespace MHW.Companion.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            services.AddMvc();

            services.RegisterWebApiDependencies();
            services.RegisterContext(Configuration);

            var builder = services.AddIdentityCore<AppUser>(o =>
            {
                o.Password.RequireNonAlphanumeric = false;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole<int>), builder.Services);
            builder.AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            services.AddAutoMapper(opt =>
            {
                opt.AddProfile<ViewModelToDataModelMappingProfile>();
                //opt.ForAllMaps((map, exp) => exp.ForAllOtherMembers(mo => mo.Ignore()));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseAuthentication()
                .UseMiddleware<ExceptionHandlingMiddleware>()
                .UseMvc();

        }
    }
}
