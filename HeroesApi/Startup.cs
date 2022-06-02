    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;



    using Newtonsoft.Json.Converters;

    using Microsoft.EntityFrameworkCore;
    using System.Net.Http;

    using NSwag;
    using NSwag.Generation.Processors.Security;
    using NSwag.AspNetCore;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System.Text;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.AspNetCore.Http;
using HeroesDal;
using HeroesServices.Middlewares;
using static HeroesApi.Models.Appsettings;
using HeroesServices.Services;
using HeroesServices;

namespace HeroesApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, HeroesDBContext db)
        {


            app.UseDeveloperExceptionPage();

     


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Create the DB if not exist yet.
            db.Database.EnsureCreated();

            //swagger
            app.UseOpenApi();
            app.UseSwaggerUi3();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            InitConnections(services);

            services.AddControllers();

            var secret = Configuration.GetSection("AppSettings").GetSection("Secret").Value;
            var key = Encoding.ASCII.GetBytes(secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddOpenApiDocument(document =>
            {
                document.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.Basic,
                    Scheme = "Bearer",
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
            });

                document.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });


            services.AddScoped<ITrainerService,TrainerService >();
            services.AddScoped<EncryptionService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddTransient<HeroesDBContext>();


            services.AddDbContext<HeroesDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SQLConnectionString"));
            });

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            // JWT
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            services.AddMvc(config =>
            {
                config.Filters.Add(typeof(ExceptionHandler));
            });
            services.AddHttpClient();
        }

        private void InitConnections(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("SQLConnectionString");
            services.AddDbContext<HeroesDBContext>(options => options.UseSqlServer(connection));
        }
    }
}