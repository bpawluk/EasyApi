using BlazorUtils.EasyApi.Server;
using static BlazorUtils.EasyApi.Tests.SUT.Contract.WithResponseRequest;

namespace BlazorUtils.EasyApi.Tests.SUT.Contract;

internal class AllResponseKindsRequestsHandler
    : HandlerBase
    , IHandle<NoResponseRequest>
    , IHandle<WithResponseRequest, Response>
{
    public Task<HttpResult> Handle(NoResponseRequest request, CancellationToken cancellationToken)
        => HandleRequest(request, request.ExpectedStatusCode);

    public Task<HttpResult<Response>> Handle(WithResponseRequest request, CancellationToken cancellationToken)
    {
        if (request.IncludeResponse)
        {
            return HandleRequest(request, request.ExpectedStatusCode);
        }
        return Task.FromResult(HttpResult<Response>.WithStatusCode(request.ExpectedStatusCode));
    }
}