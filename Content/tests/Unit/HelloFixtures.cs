using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CarterService;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace CarterService.Tests.Unit
{
    public class HelloModuleFixtures : IDisposable
    {
        private readonly HttpClient client;
        private readonly TestServer server;

        public HelloModuleFixtures()
        {
            var featureCollection = new FeatureCollection();
            featureCollection.Set<IServerAddressesFeature>(new ServerAddressesFeature());

            server = new TestServer(WebHost.CreateDefaultBuilder()
                    .UseStartup<Startup>(), featureCollection
            );

            client = server.CreateClient();
        }

        public void Dispose()
        {
            server?.Dispose();
            client?.Dispose();
        }

        [Fact]
        public async Task Hello_module_get_hello_world()
        {
            //Arrange
            const string name = "myUser";

            //Act
            var res = await client.GetAsync($"/hello/{name}")
                .ConfigureAwait(false);

            //Assert
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            Assert.Contains(name, await res.Content.ReadAsStringAsync().ConfigureAwait(false));
        }
    }
}
