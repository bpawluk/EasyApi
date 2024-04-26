using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Tests.SUT.Contract.Params;
using BlazorUtils.EasyApi.Tests.SUT.Server.Utils;

namespace BlazorUtils.EasyApi.Tests.SUT.Server.Params;

public class ParamKindsRequestsHandler
    : HandlerBase
    , IHandle<NoParamsRequest>
    , IHandle<HeaderParamRequest>
    , IHandle<RouteParamRequest>
    , IHandle<QueryStringParamRequest>
    , IHandle<BodyParamRequest>
{
    public Task<HttpResult> Handle(NoParamsRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult> Handle(HeaderParamRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult> Handle(RouteParamRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult> Handle(QueryStringParamRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult> Handle(BodyParamRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);
}