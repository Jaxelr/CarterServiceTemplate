using CarterService.Entities.Internal;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Scalar.AspNetCore;

namespace CarterService.Extensions
{
    public static class WebApplicationExtensions
    {
        internal static WebApplication MapOpenApi(this WebApplication app, AppSettings settings)
        {
            app.MapOpenApi($"{settings.RouteDefinition.Resource}/{settings.RouteDefinition.Version}.json");
            app.MapScalarApiReference($"{settings.RouteDefinition.Resource}");

            return app;
        }

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
