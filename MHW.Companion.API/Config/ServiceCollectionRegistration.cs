using AutoMapper;
using MHW.Companion.API.Auth;
using MHW.Companion.Config;
using MHW.Companion.Data.Store;
using MHW.Companion.Model.User;
using MHW.Companion.ViewModels.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;

namespace MHW.Companion.API.Config
{
    public static class ServiceCollectionRegistration
    {
        public static IServiceCollection RegisterWebApiDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAppConfiguration, AppConfiguration>();
            services.AddSingleton<IJwtFactory, JwtFactory>();

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

        public static IServiceCollection RegisterJWT(this IServiceCollection services, IConfiguration configuration, SymmetricSecurityKey key)
        {
            var jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions));
            
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            });

            return services;
        }

        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration, SymmetricSecurityKey key)
        {
            var jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(JwtConstants.Strings.JwtClaimIdentifiers.Rol, JwtConstants.Strings.JwtClaims.ApiAccess));
            });

            return services;
        }
    }
}
