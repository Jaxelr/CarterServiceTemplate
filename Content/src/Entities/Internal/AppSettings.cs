namespace CarterService.Entities;

/// <summary>
/// This is obtained from the appsettings.json on Startup
/// </summary>
public record AppSettings
{
    public CacheConfig Cache { get; init; } = new();
    public RouteDefinition RouteDefinition { get; init; } = new();
    public HealthDefinition HealthDefinition { get; init; } = new();
    public string[] ServerUrls { get; init; } = [];
}
