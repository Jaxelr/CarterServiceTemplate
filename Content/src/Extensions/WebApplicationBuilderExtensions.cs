using Carter;
using Carter.Cache;
using CarterService.Entities.Internal;
using CarterService.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi;

namespace CarterService.Extensions;

public static class WebApplicationBuilderExtensions
{
    private const string ServiceName = "Carter Service";

    /// <summary>
    /// Registers application services and the CORS policy.
    /// </summary>
    internal static WebApplicationBuilder AddApplicationServices(
        this WebApplicationBuilder builder,
        AppSettings settings,
        string corsPolicy)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(corsPolicy, policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        builder.Services.AddCarterCaching(new CachingOption(settings.Cache.CacheMaxSize));
        builder.Services.AddCarter();
        builder.Services.AddSingleton(settings); //typeof(AppSettings)
        builder.Services.AddSingleton<IHelloRepository, HelloRepository>();

        return builder;
    }

    /// <summary>
    /// Configures OpenAPI document generation.
    /// </summary>
    internal static WebApplicationBuilder AddOpenApi(this WebApplicationBuilder builder, AppSettings settings)
    {
        builder.Services.AddOpenApi(settings.RouteDefinition.Version, options =>
        {
            options.AddDocumentTransformer((document, _, _) =>
            {
                document.Info = new OpenApiInfo
                {
                    Description = ServiceName,
                    Version = settings.RouteDefinition.Version,
                };
                return System.Threading.Tasks.Task.CompletedTask;
            });
        });

        return builder;
    }

    /// <summary>
    /// Registers the configured health check.
    /// </summary>
    internal static WebApplicationBuilder AddHealthChecks(this WebApplicationBuilder builder, AppSettings settings)
    {
        builder.Services.AddHealthChecks()
        .AddCheck
        (
            settings.HealthDefinition.Name,
            () => HealthCheckResult.Healthy(settings.HealthDefinition.HealthyMessage),
            tags: settings.HealthDefinition.Tags
        );

        return builder;
    }
}
