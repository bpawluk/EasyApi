using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Tests.SUT.Contract.Params;
using BlazorUtils.EasyApi.Tests.SUT.Server.Utils;

namespace BlazorUtils.EasyApi.Tests.SUT.Server.Params;

public class ComplexParamsRequestsHandler
    : HandlerBase
    , IHandle<ComplexParamsRequest, ComplexParamsRequest.Response>
{
    public Task<HttpResult<ComplexParamsRequest.Response>> Handle(ComplexParamsRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);
}
