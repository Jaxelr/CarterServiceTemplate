using CarterService.Entities.Internal;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Scalar.AspNetCore;

namespace CarterService.Extensions
{
    public static class WebApplicationExtensions
    {
        /// <summary>
        /// Maps the OpenAPI document and reference UI.
        /// </summary>
        internal static WebApplication MapOpenApi(this WebApplication app, AppSettings settings)
        {
            app.MapOpenApi($"{settings.RouteDefinition.Resource}/{settings.RouteDefinition.Version}.json");
            app.MapScalarApiReference($"{settings.RouteDefinition.Resource}");

            return app;
        }

        /// <summary>
        /// Adds the health check endpoint to the pipeline.
        /// </summary>
        internal static WebApplication UseHealthChecks(this WebApplication app)
        {
            app.UseHealthChecks("/healthcheck", new HealthCheckOptions()
            {
                AllowCachingResponses = false,
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            return app;
        }
    }
}
