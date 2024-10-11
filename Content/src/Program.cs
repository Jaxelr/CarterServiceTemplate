using Carter;
using Carter.Cache;
using CarterService.Entities.Internal;
using CarterService.Extensions;
using CarterService.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

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

builder.Host.UseSerilog((ctx, services, config) =>
    config
    .ReadFrom.Configuration(ctx.Configuration)
    .ReadFrom.Services(services));

builder.AddSwagger(settings);

builder.Services.AddCarterCaching(new CachingOption(settings.Cache.CacheMaxSize));
builder.Services.AddCarter();

builder.Services.AddSingleton(settings); //typeof(AppSettings)
builder.Services.AddSingleton<IHelloRepository, HelloRepository>();

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

app.MapSwagger();

app.UseCarterCaching();
app.MapCarter();

await app.RunAsync();
