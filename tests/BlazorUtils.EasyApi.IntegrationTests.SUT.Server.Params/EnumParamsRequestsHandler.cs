using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.IntegrationTests.SUT.Contract.Params;
using BlazorUtils.EasyApi.IntegrationTests.SUT.Server.Utils;

namespace BlazorUtils.EasyApi.IntegrationTests.SUT.Server.Params;

public class EnumParamsRequestsHandler
    : HandlerBase
    , IHandle<EnumParamsRequest, EnumParamsRequest.Response>
{
    public Task<HttpResult<EnumParamsRequest.Response>> Handle(EnumParamsRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);
}