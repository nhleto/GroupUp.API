using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SecretSanta.API.Firestore;
using SecretSanta.API.Models;
using SecretSanta.API.Models.Interfaces;

namespace SecretSanta.API
{
    public class Startup
    {
        private const string Filepath = "../secretsantatest-4e7fc-firebase-adminsdk-sgc8u-e432b491ba.json";
        
        public Startup(IWebHostEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: false)
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<Startup>(true, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", Filepath);
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SecretSanta.API", Version = "v1" });
            });

            services.AddSwaggerGenNewtonsoftSupport();

            var config = Configuration.GetSection("Firestore").Get<FirestoreConfig>();
            
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton(config);            
            services.AddSingleton<IGroupRepository, GroupRepository>();
            services.Configure<FirestoreConfig>(Configuration.GetSection("Firestore"));
            
            services.AddSpaStaticFiles(conf =>
            {
                conf.RootPath = "wwwroot";
            });
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var firebaseProjectName = Configuration[config.ProjectId];
                    options.Authority = "https://securetoken.google.com/" + firebaseProjectName;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "https://securetoken.google.com/" + firebaseProjectName,
                        ValidateAudience = true,
                        ValidAudience = firebaseProjectName,
                        ValidateLifetime = true
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SecretSanta.API v1"));

            app.UseRouting();

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "client-app";
                if (env.IsDevelopment() || env.IsEnvironment("local"))
                {
                    var startScript = env.IsEnvironment("local") ? "start-local" : "start";
                    spa.UseAngularCliServer(npmScript: startScript);
                }
            });
        }
    }
}