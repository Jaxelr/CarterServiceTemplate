using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

[assembly: InternalsVisibleTo("CarterServiceTests")]

namespace CarterService
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseStartup<Startup>()
                        .UseIISIntegration();
                    }).Build();

            host.Run();
        }
    }
}
