using Carter.OpenApi;
using CarterService.Entities.Internal;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi;

namespace CarterService.Extensions;

public static class WebApplicationBuilderExtensions
{
    private const string ServiceName = "Carter Service";

    internal static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder, AppSettings settings)
    {
        //Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Description = ServiceName,
                Version = settings.RouteDefinition.Version
            });

            options.DocInclusionPredicate((_, description) =>
            {
                foreach (object metaData in description.ActionDescriptor.EndpointMetadata)
                {
                    if (metaData is IIncludeOpenApi)
                    {
                        return true;
                    }
                }
                return false;
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
