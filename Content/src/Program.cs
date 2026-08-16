using Carter;
using Carter.Cache;
using CarterService.Entities.Internal;
using CarterService.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

const string Policy = "DefaultPolicy";

var builder = WebApplication.CreateBuilder(args);

var settings = new AppSettings();

builder.Configuration.GetSection(nameof(AppSettings)).Bind(settings);

builder.AddApplicationServices(settings, Policy);

builder.Host.UseSerilog((ctx, services, config) =>
    config
    .ReadFrom.Configuration(ctx.Configuration)
    .ReadFrom.Services(services));

builder.AddOpenApi(settings);
builder.AddHealthChecks(settings);

var app = builder.Build();

app.UseCors(Policy);

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseHealthChecks();

app.MapOpenApi(settings);

app.UseCarterCaching();
app.MapCarter();

await app.RunAsync();
