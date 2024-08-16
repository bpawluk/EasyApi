using BlazorUtils.EasyApi.Server;

namespace BlazorUtils.EasyApi.UnitTests.Utils;

internal abstract class TestRequestHandler<RequestType>(TestResponseProvider responseProvider) : IHandle<RequestType, string>
    where RequestType : class, IRequest<string>, new()
{
    private readonly TestResponseProvider _responseProvider = responseProvider;

    public Task<HttpResult<string>> Handle(RequestType request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_responseProvider.Response);
    }
}
