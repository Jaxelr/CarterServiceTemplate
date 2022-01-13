namespace CarterService.Entities;

public record RouteDefinition
{
    public string RouteSuffix { get; init; }
    public string Version { get; init; }
}
