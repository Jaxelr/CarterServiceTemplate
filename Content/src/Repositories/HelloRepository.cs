namespace CarterService.Repositories;

public class HelloRepository : IHelloRepository
{
    /// <summary>
    /// Creates a greeting for the specified name.
    /// </summary>
    public string SayHello(string name) => $"Hello world, your name is {name} the hour cached is {System.DateTime.Now}";
}
