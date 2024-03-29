using BlazorUtils.EasyApi.Server;

namespace BlazorUtils.EasyApi.Tests.SUT.Contract;

internal class ParamKindsRequestsHandler
    : HandlerBase
    , IHandle<HeaderParamRequest>
    , IHandle<RouteParamRequest>
    , IHandle<QueryStringParamRequest>
    , IHandle<BodyParamRequest>
{
    public Task<HttpResult> Handle(HeaderParamRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult> Handle(RouteParamRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult> Handle(QueryStringParamRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult> Handle(BodyParamRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);
}