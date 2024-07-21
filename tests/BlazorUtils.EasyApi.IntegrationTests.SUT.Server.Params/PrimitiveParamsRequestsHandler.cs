using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.IntegrationTests.SUT.Contract.Params;
using BlazorUtils.EasyApi.IntegrationTests.SUT.Server.Utils;

namespace BlazorUtils.EasyApi.IntegrationTests.SUT.Server.Params;

public class PrimitiveParamsRequestsHandler
    : HandlerBase
    , IHandle<IntegralParamsRequest, IntegralParamsRequest.Response>
    , IHandle<FloatingParamsRequest, FloatingParamsRequest.Response>
    , IHandle<BooleanParamsRequest, BooleanParamsRequest.Response>
    , IHandle<CharacterParamsRequest, CharacterParamsRequest.Response>
{
    public Task<HttpResult<IntegralParamsRequest.Response>> Handle(IntegralParamsRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult<FloatingParamsRequest.Response>> Handle(FloatingParamsRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult<BooleanParamsRequest.Response>> Handle(BooleanParamsRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult<CharacterParamsRequest.Response>> Handle(CharacterParamsRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);
}