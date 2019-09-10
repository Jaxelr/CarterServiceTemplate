using System.Threading.Tasks;
using Carter;
using CarterService.Entities;

namespace CarterService.Modules
{
    public class MainModule : CarterModule
    {
        public MainModule(AppSettings appSettings)
        {
            Get("/", (req, res, routeData) =>
            {
                res.Redirect(appSettings.RouteDefinition.RoutePrefix);

                return Task.CompletedTask;
            });
        }
    }
}
