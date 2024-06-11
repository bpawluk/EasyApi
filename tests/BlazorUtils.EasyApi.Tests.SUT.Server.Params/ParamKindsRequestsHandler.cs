using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Tests.SUT.Contract.Params;
using BlazorUtils.EasyApi.Tests.SUT.Server.Utils;

namespace BlazorUtils.EasyApi.Tests.SUT.Server.Params;

public class ParamKindsRequestsHandler
    : HandlerBase
    , IHandle<NoParamsRequest>
    , IHandle<HeaderParamRequest, HeaderParamRequest.Response>
    , IHandle<RouteParamRequest, RouteParamRequest.Response>
    , IHandle<QueryStringParamRequest, QueryStringParamRequest.Response>
    , IHandle<BodyParamRequest, BodyParamRequest.Response>
{
    public Task<HttpResult> Handle(NoParamsRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult<HeaderParamRequest.Response>> Handle(HeaderParamRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult<RouteParamRequest.Response>> Handle(RouteParamRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult<QueryStringParamRequest.Response>> Handle(QueryStringParamRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult<BodyParamRequest.Response>> Handle(BodyParamRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);
}