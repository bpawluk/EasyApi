using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Tests.SUT.Contract;
using BlazorUtils.EasyApi.Tests.SUT.Contract.ParamTypes;

namespace BlazorUtils.EasyApi.Tests.SUT.Server.Handlers.ParamTypes;

internal class SystemParamsRequestsHandler
    : HandlerBase
    , IHandle<GuidParamsRequest, GuidParamsRequest.Response>
    , IHandle<StringParamsRequest, StringParamsRequest.Response>
    , IHandle<UriParamsRequest, UriParamsRequest.Response>
{
    public Task<HttpResult<GuidParamsRequest.Response>> Handle(GuidParamsRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult<StringParamsRequest.Response>> Handle(StringParamsRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult<UriParamsRequest.Response>> Handle(UriParamsRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);
}