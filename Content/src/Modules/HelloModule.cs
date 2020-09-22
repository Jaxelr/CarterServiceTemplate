using Carter;
using Carter.Request;
using CarterService.Entities;
using CarterService.Extensions;
using CarterService.Repository;

namespace CarterService.Modules
{
    public class HelloModule : CarterModule
    {
        public HelloModule(IHelloRepository repository, AppSettings settings)
        {
            int cacheTimespan = settings.Cache.CacheTimespan;

            Get<GetHello>("/Hello/{name}", (req, res) =>
            {
                string name = req.RouteValues.As<string>("name");

                return res.ExecHandler(cacheTimespan, () => repository.SayHello(name));
            });
        }
    }
}
