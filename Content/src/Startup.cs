using System.Collections.Generic;
using Carter;
using Carter.Cache;
using CarterService.Entities;
using CarterService.Repository;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace CarterService
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        private readonly AppSettings settings;

        private const string ServiceName = "CarterService";

        private static string Policy => "DefaultPolicy";

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables();

            Configuration = builder.Build();

            //Extract the AppSettings information from the appsettings config.
            settings = new AppSettings();
            Configuration.GetSection(nameof(AppSettings)).Bind(settings);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(settings); //typeof(AppSettings)

            services.AddSingleton<IHelloRepository>(new HelloRepository());

            //Change Cors as needed.
            services.AddCors(options =>
            {
                options.AddPolicy(Policy,
                builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            services.AddCarterCaching(new CachingOption(settings.Cache.CacheMaxSize));
            services.AddCarter(options => options.OpenApi = GetOpenApiOptions(settings));

            services.AddLogging(opt =>
            {
                opt.ClearProviders();
                opt.AddConsole();
                opt.AddDebug();
                opt.AddConfiguration(Configuration.GetSection("Logging"));
            });

            //HealthChecks
            services.AddHealthChecks()
                    .AddCheck(settings.HealthDefinition.Name, () => HealthCheckResult.Healthy(settings.HealthDefinition.HealthyMessage), tags: settings.HealthDefinition.Tags);
        }

        public void Configure(IApplicationBuilder app, AppSettings appSettings)
        {
            app.UseCors(Policy);

            app.UseRouting();

            app.UseSwaggerUI(opt =>
            {
                opt.RoutePrefix = appSettings.RouteDefinition.RoutePrefix;
                opt.SwaggerEndpoint(appSettings.RouteDefinition.SwaggerEndpoint, ServiceName);
            });

            app.UseHealthChecks(settings.HealthDefinition.Endpoint, new HealthCheckOptions()
            {
                AllowCachingResponses = false,
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseCarterCaching();
            app.UseEndpoints(builder => builder.MapCarter());
        }

        private static OpenApiOptions GetOpenApiOptions(AppSettings settings) =>
        new()
        {
            DocumentTitle = ServiceName,
            ServerUrls = settings.ServerUrls,
            Securities = new Dictionary<string, OpenApiSecurity>()
        };
    }
}
