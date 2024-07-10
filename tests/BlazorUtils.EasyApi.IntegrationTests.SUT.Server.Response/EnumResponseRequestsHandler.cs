using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.IntegrationTests.SUT.Contract.Response;

namespace BlazorUtils.EasyApi.IntegrationTests.SUT.Server.Response;

public class EnumResponseRequestsHandler
    : IHandle<EnumResponseRequest, Time>
    , IHandle<NullableEnumResponseRequest, Time?>
{
    public Task<HttpResult<Time>> Handle(EnumResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<Time>.Ok(request.ExpectedResponse));

    public Task<HttpResult<Time?>> Handle(NullableEnumResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<Time?>.Ok(request.ExpectedResponse));
}