using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CarterService.Repository;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CarterService.Tests.Unit;

public class HelloTests : IDisposable
{
    private readonly HttpClient client;

    public HelloTests()
    {
        var server = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(builder => builder.ConfigureServices
        (
                /// Mock, if needed
                services => services.AddSingleton<IHelloRepository, HelloRepository>())
        );

        client = server.CreateClient();
    }

    public void Dispose()
    {
        client?.Dispose();
        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task Hello_module_get_hello_world()
    {
        //Arrange
        const string name = "myUser";

        //Act
        var res = await client.GetAsync($"/hello/{name}");

        //Assert
        Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        Assert.Contains(name, await res.Content.ReadAsStringAsync());
    }
}
