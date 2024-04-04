using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Tests.SUT.Contract;
using BlazorUtils.EasyApi.Tests.SUT.Contract.ParamTypes;

namespace BlazorUtils.EasyApi.Tests.SUT.Server.Handlers.ParamTypes;

internal class EnumParamsRequestsHandler
    : HandlerBase
    , IHandle<EnumParamsRequest, EnumParamsRequest.Response>
{
    public Task<HttpResult<EnumParamsRequest.Response>> Handle(EnumParamsRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);
}