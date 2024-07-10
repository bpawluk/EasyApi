using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.IntegrationTests.SUT.Contract.Params;
using BlazorUtils.EasyApi.IntegrationTests.SUT.Server.Utils;

namespace BlazorUtils.EasyApi.IntegrationTests.SUT.Server.Params;

public class SystemParamsRequestsHandler
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