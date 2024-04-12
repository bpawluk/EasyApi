using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Tests.SUT.Contract.ResponseTypes;

namespace BlazorUtils.EasyApi.Tests.SUT.Server.Handlers.ResponseTypes;

internal class EnumResponseRequestsHandler
    : HandlerBase
    , IHandle<EnumResponseRequest, Time>
    , IHandle<NullableEnumResponseRequest, Time?>
{
    public Task<HttpResult<Time>> Handle(EnumResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<Time>.Ok(Time.Day));

    public Task<HttpResult<Time?>> Handle(NullableEnumResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<Time?>.Ok(request.ExpectValue ? Time.Night : null));
}