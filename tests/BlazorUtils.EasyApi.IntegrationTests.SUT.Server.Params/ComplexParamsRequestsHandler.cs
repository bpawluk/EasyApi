using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.IntegrationTests.SUT.Contract.Params;
using BlazorUtils.EasyApi.IntegrationTests.SUT.Server.Utils;

namespace BlazorUtils.EasyApi.IntegrationTests.SUT.Server.Params;

public class ComplexParamsRequestsHandler
    : HandlerBase
    , IHandle<ComplexParamsRequest, ComplexParamsRequest.Response>
{
    public Task<HttpResult<ComplexParamsRequest.Response>> Handle(ComplexParamsRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);
}
