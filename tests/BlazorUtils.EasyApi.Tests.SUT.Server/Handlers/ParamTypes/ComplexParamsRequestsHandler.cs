using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Tests.SUT.Contract.ParamTypes;

namespace BlazorUtils.EasyApi.Tests.SUT.Server.Handlers.ParamTypes;

internal class ComplexParamsRequestsHandler
    : HandlerBase
    , IHandle<ComplexParamsRequest, ComplexParamsRequest.Response>
{
    public Task<HttpResult<ComplexParamsRequest.Response>> Handle(ComplexParamsRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);
}
