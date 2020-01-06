using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CarterService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseHealthChecks("/healthcheck")
                        .UseStartup<Startup>()
                        .UseIISIntegration();
                    })
                    .Build();

            host.Run();
        }
    }
}