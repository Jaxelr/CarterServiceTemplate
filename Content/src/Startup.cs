using System.Collections.Generic;
using System.Threading.Tasks;
using Carter;
using CarterService.Cache;
using CarterService.Entities;
using CarterService.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.HealthChecks;

namespace CarterService
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        private readonly AppSettings settings;

        private const string ServiceName = "CarterService";

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
              .AddEnvironmentVariables();

            Configuration = builder.Build();

            //Extract the AppSettings information from the appsettings config.
            settings = new AppSettings();
            Configuration.GetSection(nameof(AppSettings)).Bind(settings);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //HealthChecks
            services.AddHealthChecks(checks =>
            {
                checks.AddValueTaskCheck("HTTP Endpoint", () => new
                    ValueTask<IHealthCheckResult>(HealthCheckResult.Healthy("Ok")));
            });

            services.AddSingleton(settings); //AppSettings type
            services.AddSingleton<Store>();

            services.AddSingleton<IHelloRepository>(new HelloRepository());

            services.AddCarter(options =>
            {
                options.OpenApi = GetOpenApiOptions(settings);
            });

            services.AddLogging();

            services.AddMemoryCache();
        }

        public void Configure(IApplicationBuilder app, AppSettings appSettings)
        {
            app.UseRouting();

            app.UseSwaggerUI(opt =>
            {
                opt.RoutePrefix = appSettings.RouteDefinition.RoutePrefix;
                opt.SwaggerEndpoint(appSettings.RouteDefinition.SwaggerEndpoint, ServiceName);
            });

            app.UseEndpoints(builder => builder.MapCarter());
        }

        private OpenApiOptions GetOpenApiOptions(AppSettings settings) =>
        new OpenApiOptions()
        {
            DocumentTitle = ServiceName,
            ServerUrls = settings.ServerUrls,
            Securities = new Dictionary<string, OpenApiSecurity>()
        };
    }
}
