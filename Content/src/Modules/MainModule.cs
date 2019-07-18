using System.Threading.Tasks;
using CarterService.Entities;
using Carter;

namespace CarterService.Modules
{
    public class MainModule : CarterModule
    {
        public MainModule()
        {
            Get("/", (req, res, routeData) =>
            {
                res.Redirect(RouteDefinition.RoutePrefix);

                return Task.CompletedTask;
            });
        }
    }
}
