namespace CarterService.Repositories;

public interface IHelloRepository
{
    /// <summary>
    /// Creates a greeting for the specified name.
    /// </summary>
    string SayHello(string name);
}
