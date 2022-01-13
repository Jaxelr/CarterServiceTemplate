namespace CarterService.Entities;

/// <summary>
/// This is obtained from the appsettings.json on Startup
/// </summary>
public record AppSettings
{
    public CacheConfig Cache { get; init; }
    public RouteDefinition RouteDefinition { get; init; }
    public HealthDefinition HealthDefinition { get; init; }
    public string[] ServerUrls { get; init; }
}
