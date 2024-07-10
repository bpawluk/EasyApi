using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.IntegrationTests.SUT.Contract.Response;
using BlazorUtils.EasyApi.IntegrationTests.SUT.Server.Utils;

namespace BlazorUtils.EasyApi.IntegrationTests.SUT.Server.Response;

public class ResponseKindsRequestsHandler
    : HandlerBase
    , IHandle<NoResponseRequest>
    , IHandle<WithResponseRequest, WithResponseRequest.Response>
{
    public Task<HttpResult> Handle(NoResponseRequest request, CancellationToken cancellationToken)
        => HandleRequest(request, request.ExpectedStatusCode);

    public Task<HttpResult<WithResponseRequest.Response>> Handle(WithResponseRequest request, CancellationToken cancellationToken)
    {
        if (request.IncludeResponse)
        {
            return HandleRequest(request, request.ExpectedStatusCode);
        }
        return Task.FromResult(HttpResult<WithResponseRequest.Response>.WithStatusCode(request.ExpectedStatusCode));
    }
}