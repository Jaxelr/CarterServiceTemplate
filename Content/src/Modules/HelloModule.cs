using Carter;
using Carter.OpenApi;
using CarterService.Entities.Internal;
using CarterService.Extensions;
using CarterService.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CarterService.Modules;

public class HelloModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app) =>
        app.MapGet("/Hello/{name}",
            (HttpContext ctx, string name, AppSettings settings, IHelloRepository repository) =>
            ctx.ExecHandler(settings.Cache.CacheTimespan, () => repository.SayHello(name)))
            .Produces<string>(200)
            .Produces<FailedResponse>(500)
            .WithName("GetHello")
            .WithTags("Hello")
            .IncludeInOpenApi();
}
