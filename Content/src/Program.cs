using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
                        .UseHealthChecks("/healthcheck")
                        .UseStartup<Startup>()
                        .UseIISIntegration();
                    })
                    .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.AddConsole();
                    })
                    .Build();

            host.Run();
        }
    }
}
