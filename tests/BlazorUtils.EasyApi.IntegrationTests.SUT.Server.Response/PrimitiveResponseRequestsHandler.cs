﻿using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.IntegrationTests.SUT.Contract.Response;

namespace BlazorUtils.EasyApi.IntegrationTests.SUT.Server.Response;

public class PrimitiveResponseRequestsHandler
    : IHandle<IntegralResponseRequest, int>
    , IHandle<NullableIntegralResponseRequest, int?>
    , IHandle<FloatingResponseRequest, float>
    , IHandle<NullableFloatingResponseRequest, float?>
    , IHandle<BooleanResponseRequest, bool>
    , IHandle<NullableBooleanResponseRequest, bool?>
    , IHandle<CharacterResponseRequest, char>
    , IHandle<NullableCharacterResponseRequest, char?>
{
    public Task<HttpResult<int>> Handle(IntegralResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<int>.Ok(request.ExpectedResponse));

    public Task<HttpResult<int?>> Handle(NullableIntegralResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<int?>.Ok(request.ExpectedResponse));

    public Task<HttpResult<float>> Handle(FloatingResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<float>.Ok(request.ExpectedResponse));

    public Task<HttpResult<float?>> Handle(NullableFloatingResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<float?>.Ok(request.ExpectedResponse));

    public Task<HttpResult<bool>> Handle(BooleanResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<bool>.Ok(request.ExpectedResponse));

    public Task<HttpResult<bool?>> Handle(NullableBooleanResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<bool?>.Ok(request.ExpectedResponse));

    public Task<HttpResult<char>> Handle(CharacterResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<char>.Ok(request.ExpectedResponse));

    public Task<HttpResult<char?>> Handle(NullableCharacterResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<char?>.Ok(request.ExpectedResponse));
}
