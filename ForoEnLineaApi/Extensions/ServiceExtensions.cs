using FluentValidation.AspNetCore;
using ForoEnLineaApi.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

namespace ForoEnLineaApi.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSetting = configuration.GetSection(Constants.JwtSettingsCons).Get<JwtSettings>();

            if (jwtSetting.Enabled)
            {
                services.AddAuthentication(config =>
                {
                    config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                   .AddJwtBearer(config =>
                   {
                       config.RequireHttpsMetadata = false;
                       config.SaveToken = true;
                       config.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuerSigningKey = false,
                           RequireSignedTokens = true,
                           ValidateIssuer = true,
                           ValidateAudience = jwtSetting.ValidateAudience,
                           ValidAudience = "Swagger", //POR VER
                           ValidateLifetime = true,
                           ClockSkew = TimeSpan.Zero,
                           SignatureValidator = delegate (string token, TokenValidationParameters parameters)
                           {
                               var jwt = new JwtSecurityToken(token);
                               return jwt;
                           }
                       };
                   });
            }

            return services;
        }

        public static IServiceCollection AddCustomAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSetting = configuration.GetSection(Constants.JwtSettingsCons).Get<JwtSettings>();

            services.AddAuthorization(c =>
            {
                if (jwtSetting.Enabled)
                {
                    c.AddPolicy(Constants.GlobalOAuthPolicyName, p =>
                    {
                        p.RequireAuthenticatedUser();
                    });
                }
            });

            return services;
        }

        public static IServiceCollection AddCustomMVC(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection(Constants.JwtSettingsCons).Get<JwtSettings>();

            services.AddMemoryCache();
            services.AddControllers(options =>
            {
                if (jwtSettings.Enabled)
                {
                    options.Filters.Add(new AuthorizeFilter(Constants.GlobalOAuthPolicyName));
                }
            })
                .AddNewtonsoftJson();
                //.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ICurrentUser>());
            services.AddCors(options =>
            {
                options.AddPolicy(Constants.CorsPolicyName,
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithExposedHeaders("Content-Disposition"));
            });
            services.AddHttpContextAccessor();
            services.AddResponseCompression();
            return services;
        }

        public static IServiceCollection AddLayersDependencyInjections(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }

        public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration);
            services.Configure<JwtSettings>(configuration.GetSection(Constants.JwtSettingsCons));
            return services;
        }

    }
}
