using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Tests.SUT.Contract.Params;
using BlazorUtils.EasyApi.Tests.SUT.Server.Utils;

namespace BlazorUtils.EasyApi.Tests.SUT.Server.Params;

public class EnumParamsRequestsHandler
    : HandlerBase
    , IHandle<EnumParamsRequest, EnumParamsRequest.Response>
{
    public Task<HttpResult<EnumParamsRequest.Response>> Handle(EnumParamsRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);
}