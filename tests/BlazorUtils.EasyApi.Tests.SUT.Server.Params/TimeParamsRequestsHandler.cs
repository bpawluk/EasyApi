using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Tests.SUT.Contract.Params;
using BlazorUtils.EasyApi.Tests.SUT.Server.Utils;

namespace BlazorUtils.EasyApi.Tests.SUT.Server.Params;

public class TimeParamsRequestsHandler
    : HandlerBase
    , IHandle<TimeParamsRequest, TimeParamsRequest.Response>
{
    public Task<HttpResult<TimeParamsRequest.Response>> Handle(TimeParamsRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);
}