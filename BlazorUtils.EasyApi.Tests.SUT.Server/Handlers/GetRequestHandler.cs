using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Tests.SUT.Contract;

namespace BlazorUtils.EasyApi.Tests.SUT.Server.Handlers;

public class GetRequestHandler : IHandle<GetRequest, GetRequest.Response>
{
    public Task<GetRequest.Response> Handle(GetRequest request, CancellationToken cancellationToken)
    {
        var response = new GetRequest.Response() { Number = request.Number + 1 };
        return Task.FromResult(response);
    }
}
