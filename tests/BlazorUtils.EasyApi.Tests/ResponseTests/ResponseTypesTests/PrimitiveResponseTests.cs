using BlazorUtils.EasyApi.Tests.SUT.Contract.ResponseTypes;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace BlazorUtils.EasyApi.Tests.ResponseTests.ResponseTypesTests;

public abstract class PrimitiveResponseTests(TestsFixture fixture) : TestsBase(fixture)
{
    [Fact]
    public async Task Request_ForResponse_OfIntegralType()
    {
        var request = new IntegralResponseRequest();
        var result = await CallHttp<IntegralResponseRequest, int>(request);
        Assert.Equal(int.MinValue, result);
    }

    [Fact]
    public async Task Request_ForResponse_OfNullableIntegralType_WithValue()
    {
        var request = new NullableIntegralResponseRequest() { ExpectValue = true };
        var result = await CallHttp<NullableIntegralResponseRequest, int?>(request);
        Assert.Equal(int.MaxValue, result);
    }

    [Fact]
    public async Task Request_ForResponse_OfNullableIntegralType_NoValue()
    {
        var request = new NullableIntegralResponseRequest() { ExpectValue = false };
        var caller = GetCaller<NullableIntegralResponseRequest, int?>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }

    [Fact]
    public async Task Request_ForResponse_OfFloatingType()
    {
        var request = new FloatingResponseRequest();
        var result = await CallHttp<FloatingResponseRequest, float>(request);
        Assert.Equal(float.MinValue, result);
    }

    [Fact]
    public async Task Request_ForResponse_OfNullableFloatingType_WithValue()
    {
        var request = new NullableFloatingResponseRequest() { ExpectValue = true };
        var result = await CallHttp<NullableFloatingResponseRequest, float?>(request);
        Assert.Equal(float.MaxValue, result);
    }

    [Fact]
    public async Task Request_ForResponse_OfNullableFloatingType_NoValue()
    {
        var request = new NullableFloatingResponseRequest() { ExpectValue = false };
        var caller = GetCaller<NullableFloatingResponseRequest, float?>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }

    [Fact]
    public async Task Request_ForResponse_OfBooleanType()
    {
        var request = new BooleanResponseRequest();
        var result = await CallHttp<BooleanResponseRequest, bool>(request);
        Assert.False(result);
    }

    [Fact]
    public async Task Request_ForResponse_OfNullableBooleanType_WithValue()
    {
        var request = new NullableBooleanResponseRequest() { ExpectValue = true };
        var result = await CallHttp<NullableBooleanResponseRequest, bool?>(request);
        Assert.True(result);
    }

    [Fact]
    public async Task Request_ForResponse_OfNullableBooleanType_NoValue()
    {
        var request = new NullableBooleanResponseRequest() { ExpectValue = false };
        var caller = GetCaller<NullableBooleanResponseRequest, bool?>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }

    [Fact]
    public async Task Request_ForResponse_OfCharacterType()
    {
        var request = new CharacterResponseRequest();
        var result = await CallHttp<CharacterResponseRequest, char>(request);
        Assert.Equal('\0', result);
    }

    [Fact]
    public async Task Request_ForResponse_OfNullableCharacterType_WithValue()
    {
        var request = new NullableCharacterResponseRequest() { ExpectValue = true };
        var result = await CallHttp<NullableCharacterResponseRequest, char?>(request);
        Assert.Equal('\x006A', result);
    }

    [Fact]
    public async Task Request_ForResponse_OfNullableCharacterType_NoValue()
    {
        var request = new NullableCharacterResponseRequest() { ExpectValue = false };
        var caller = GetCaller<NullableCharacterResponseRequest, char?>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }
}

public class Client_PrimitiveResponseTests(TestsFixture fixture) : PrimitiveResponseTests(fixture)
{
    protected override ICall<Request> GetCaller<Request>() => _client.Services.GetRequiredService<ICall<Request>>();

    protected override ICall<Request, Response> GetCaller<Request, Response>() => _client.Services.GetRequiredService<ICall<Request, Response>>();
}

public class Server_PrimitiveResponseTests(TestsFixture fixture) : PrimitiveResponseTests(fixture)
{
    protected override ICall<Request> GetCaller<Request>() => _server.Services.GetRequiredService<ICall<Request>>();

    protected override ICall<Request, Response> GetCaller<Request, Response>() => _server.Services.GetRequiredService<ICall<Request, Response>>();
}
