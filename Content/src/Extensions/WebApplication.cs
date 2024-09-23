using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace CarterService.Extensions
{
    public static class WebApplicationExtensions
    {
        internal static WebApplication MapSwagger(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

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
