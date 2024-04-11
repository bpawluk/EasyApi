using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Tests.SUT.Contract.ResponseTypes;

namespace BlazorUtils.EasyApi.Tests.SUT.Server.Handlers.ResponseTypes;

internal class PrimitiveResponseRequestsHandler
    : HandlerBase
    , IHandle<IntegralResponseRequest, int>
    , IHandle<NullableIntegralResponseRequest, int?>
    , IHandle<FloatingResponseRequest, float>
    , IHandle<NullableFloatingResponseRequest, float?>
    , IHandle<BooleanResponseRequest, bool>
    , IHandle<NullableBooleanResponseRequest, bool?>
    , IHandle<CharacterResponseRequest, char>
    , IHandle<NullableCharacterResponseRequest, char?>
{
    public Task<HttpResult<int>> Handle(IntegralResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<int>.Ok(int.MinValue));

    public Task<HttpResult<int?>> Handle(NullableIntegralResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<int?>.Ok(request.ExpectValue ? int.MaxValue : null));

    public Task<HttpResult<float>> Handle(FloatingResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<float>.Ok(float.MinValue));

    public Task<HttpResult<float?>> Handle(NullableFloatingResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<float?>.Ok(request.ExpectValue ? float.MaxValue : null));

    public Task<HttpResult<bool>> Handle(BooleanResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<bool>.Ok(false));

    public Task<HttpResult<bool?>> Handle(NullableBooleanResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<bool?>.Ok(request.ExpectValue ? true : null));

    public Task<HttpResult<char>> Handle(CharacterResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<char>.Ok('\0'));

    public Task<HttpResult<char?>> Handle(NullableCharacterResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<char?>.Ok(request.ExpectValue ? '\x006A' : null));
}
