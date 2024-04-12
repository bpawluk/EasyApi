using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Tests.SUT.Contract.ResponseTypes;

namespace BlazorUtils.EasyApi.Tests.SUT.Server.Handlers.ResponseTypes;

internal class SystemResponseRequestsHandler
    : HandlerBase
    , IHandle<GuidResponseRequest, Guid>
    , IHandle<NullableGuidResponseRequest, Guid?>
    , IHandle<StringResponseRequest, string>
    , IHandle<UriResponseRequest, Uri>
{
    public Task<HttpResult<Guid>> Handle(GuidResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<Guid>.Ok(request.ExpectedResponse));

    public Task<HttpResult<Guid?>> Handle(NullableGuidResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<Guid?>.Ok(request.ExpectedResponse));

    public Task<HttpResult<string>> Handle(StringResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<string>.Ok(request.ExpectedResponse));

    public Task<HttpResult<Uri>> Handle(UriResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<Uri>.Ok(request.ExpectedResponse));

}