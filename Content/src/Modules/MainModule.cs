using System.Threading.Tasks;
using Carter;
using CarterService.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CarterService.Modules;

public class MainModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app) => app.MapGet("/", (HttpContext ctx, AppSettings app) =>
     {
         ctx.Response.Redirect(app.RouteDefinition.RouteSuffix);

         return Task.CompletedTask;
     });
}
