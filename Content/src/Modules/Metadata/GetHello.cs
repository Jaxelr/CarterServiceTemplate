using Carter.OpenApi;
using CarterService.Entities;

namespace CarterService.Modules
{
    public class GetHello : RouteMetaData
    {
        private const string TagInfo = "Hello";
        private const string DescriptionInfo = "Returns a message with hello world";

        public override RouteMetaDataResponse[] Responses { get; } =
        {
            new RouteMetaDataResponse
            {
                Code = 200,
                Description = "A message with hello world",
                Response = typeof(string)
            },
            new RouteMetaDataResponse
            {
                Code = 500,
                Description = "A response if an internal server error is detected",
                Response = typeof(FailedResponse),
            }
        };

        public override string Description => DescriptionInfo;

        public override string Tag => TagInfo;

        public override string OperationId => nameof(GetHello);
    }
}
