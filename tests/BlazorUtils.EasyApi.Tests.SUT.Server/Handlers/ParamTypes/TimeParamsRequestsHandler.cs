using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Tests.SUT.Contract;
using BlazorUtils.EasyApi.Tests.SUT.Contract.ParamTypes;

namespace BlazorUtils.EasyApi.Tests.SUT.Server.Handlers.ParamTypes;

internal class TimeParamsRequestsHandler
    : HandlerBase
    , IHandle<TimeParamsRequest, TimeParamsRequest.Response>
{
    public Task<HttpResult<TimeParamsRequest.Response>> Handle(TimeParamsRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);
}