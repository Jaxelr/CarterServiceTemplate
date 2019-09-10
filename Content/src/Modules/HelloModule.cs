using Carter;
using Carter.Request;
using CarterService.Cache;
using CarterService.Extensions;
using CarterService.Repository;

namespace CarterService.Modules
{
    public class HelloModule : CarterModule
    {
        public HelloModule(IHelloRepository repository, Store store)
        {
            Get<GetHello>("/Hello/{name}", (req, res, routeData) =>
            {
                string name = routeData.As<string>("name");

                return res.ExecHandler(name, store, () => repository.SayHello(name));
            });
        }
    }
}
