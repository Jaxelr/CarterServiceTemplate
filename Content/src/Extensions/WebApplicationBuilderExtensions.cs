using CarterService.Entities.Internal;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi;


namespace CarterService.Extensions;

public static class WebApplicationBuilderExtensions
{
    private const string ServiceName = "Carter Service";

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
