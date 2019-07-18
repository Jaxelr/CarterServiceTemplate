using CarterService.Cache;
using CarterService.Repository;
using CarterService.Extensions;
using Carter;
using Carter.Request;

namespace CarterService.Modules
{
    public class HelloModule : CarterModule
    {
#pragma warning disable IDE0052 // Remove unread private members
        private readonly IHelloRepository repository;
        private readonly Store store;
#pragma warning restore IDE0052 // Remove unread private members

        public HelloModule(IHelloRepository repository, Store store)
        {
            this.repository = repository;
            this.store = store;

            Get<GetHello>("/Hello/{name}", (req, res, routeData) =>
            {
                string name = routeData.As<string>("name");

                return res.ExecHandler(name, store, () => repository.SayHello(name));
            });
        }
    }
}
