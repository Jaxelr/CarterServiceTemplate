namespace CarterService.Entities;

public record HealthDefinition
{
    public string Endpoint { get; init; }
    public string Name { get; init; }
    public string HealthyMessage { get; init; }
    public string[] Tags { get; init; }
}
