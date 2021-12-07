namespace CarterService.Entities;

public class HealthDefinition
{
    public string Endpoint { get; set; }
    public string Name { get; set; }
    public string HealthyMessage { get; set; }
    public string[] Tags { get; set; }
}
