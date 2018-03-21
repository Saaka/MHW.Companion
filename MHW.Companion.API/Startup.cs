using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MHW.Companion.Data.Config;
using MHW.Companion.API.Config;
using Microsoft.IdentityModel.Tokens;
using System;
using FluentValidation.AspNetCore;
using MHW.Companion.ViewModels.Validations;

namespace MHW.Companion.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private SymmetricSecurityKey _signingKey;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _signingKey = SymetricKeyGenerator.CreateKey(Configuration);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddValidation();

            services
                .AddAuth(Configuration, _signingKey)
                .RegisterWebApiDependencies()
                .RegisterContext(Configuration)
                .RegisterIdentity()
                .RegisterJWT(Configuration, _signingKey)
                .RegisterAutoMapper();
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
