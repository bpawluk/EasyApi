using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.IntegrationTests.SUT.Contract.Params;
using BlazorUtils.EasyApi.IntegrationTests.SUT.Server.Utils;

namespace BlazorUtils.EasyApi.IntegrationTests.SUT.Server.Params;

public class TimeParamsRequestsHandler
    : HandlerBase
    , IHandle<TimeParamsRequest, TimeParamsRequest.Response>
{
    public Task<HttpResult<TimeParamsRequest.Response>> Handle(TimeParamsRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);
}