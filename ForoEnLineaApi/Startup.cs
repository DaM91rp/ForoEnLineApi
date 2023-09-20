using ForoEnLineaApi.Interfaces;
using ForoEnLineaApi.DataAccess.DataBase;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using ForoEnLineaApi.Interfaces.Repositories;
using ForoEnLineaApi.DataAccess.Repositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ForoEnLineaApi.Infrasestructure;
using ForoEnLineaApi.Extensions;
using ForoEnLineaApi.Interfaces.Mapping;
using ForoEnLineaApi.Servicios.TokenService;
using ForoEnLineaApi.Utils;

namespace ForoEnLineaApi
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            var connectionString = Configuration.GetConnectionString(Constants.ConnectionStringSqlServer);

            services.AddSingleton<IDataBase>(sp => new SqlDataBase(connectionString));
            services.AddTransient<IHilosRepository,HiloRepository>();
            services.AddTransient<IUsuarioRepositiry, UsuarioRepositiry>();
            services.AddTransient<IValidacionesRepository,ValidacionesRepository>();
            services.AddTransient<ILoginRepository,LoginRepository>();

            services.AddSingleton<ICryptography, AesEncryption>();
            services.AddTransient<IJwtService, JwtService>();
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<ITokenRepository, TokenRepository>();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddLayersDependencyInjections(Configuration);
            //services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddCustomAuthentication(Configuration);
            services.AddCustomAuthorization(Configuration);
            services.AddCustomMVC(Configuration);
            services.AddCustomOptions(Configuration);
            services.AddTransient<ICurrentUser, CurrentUser>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(Constants.CorsPolicyName);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoint =>
            {
                endpoint.MapControllers();
            });
        }

    }
}
