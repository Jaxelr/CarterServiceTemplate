using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

[assembly: InternalsVisibleTo("CarterService.Tests")]
const string ServiceName = "Carter Service";
const string Policy = "DefaultPolicy";

var builder = WebApplication.CreateBuilder(args);

var settings = new AppSettings();

builder.Configuration.GetSection(nameof(AppSettings)).Bind(settings);

builder.Services.AddCors(options =>
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

builder.Services.AddLogging(opt =>
{
    opt.ClearProviders();
    opt.AddConsole();
    opt.AddDebug();
    opt.AddConfiguration(builder.Configuration.GetSection("Logging"));
});

builder.Services.AddCarterCaching(new CachingOption(settings.Cache.CacheMaxSize));
builder.Services.AddCarter(options => options.OpenApi = GetOpenApiOptions(settings));
builder.Services.AddSingleton(settings); //typeof(AppSettings)
builder.Services.AddSingleton<IHelloRepository>(new HelloRepository());

//HealthChecks
builder.Services.AddHealthChecks()
   .AddCheck
   (
        settings.HealthDefinition.Name,
        () => HealthCheckResult.Healthy(settings.HealthDefinition.HealthyMessage),
        tags: settings.HealthDefinition.Tags
   );

var app = builder.Build();

app.UseCors(Policy);

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSwaggerUI(opt =>
{
    opt.RoutePrefix = settings.RouteDefinition.RoutePrefix;
    opt.SwaggerEndpoint(settings.RouteDefinition.SwaggerEndpoint, ServiceName);
});

app.UseHealthChecks("/healthcheck", new HealthCheckOptions()
{
    AllowCachingResponses = false,
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseCarterCaching();
app.UseEndpoints(builder => builder.MapCarter());

app.Run();

static OpenApiOptions GetOpenApiOptions(AppSettings settings) =>
        new()
        {
            DocumentTitle = ServiceName,
            ServerUrls = settings.ServerUrls,
            Securities = new Dictionary<string, OpenApiSecurity>()
        };
