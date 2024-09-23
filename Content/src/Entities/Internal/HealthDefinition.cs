namespace CarterService.Entities;

public record HealthDefinition
{
    public string Endpoint { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string HealthyMessage { get; init; } = string.Empty;
    public string[] Tags { get; init; } = [];
}
