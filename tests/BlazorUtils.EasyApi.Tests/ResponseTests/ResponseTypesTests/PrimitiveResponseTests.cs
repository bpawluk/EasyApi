using BlazorUtils.EasyApi.Tests.SUT.Contract.Response;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace BlazorUtils.EasyApi.Tests.ResponseTests.ResponseTypesTests;

public abstract class PrimitiveResponseTests(TestsFixture fixture) : TestsBase(fixture)
{
    [Theory]
    [InlineData(int.MinValue)]
    [InlineData(0)]
    [InlineData(int.MaxValue)]
    public async Task Request_ForResponse_OfIntegralType(int expectedResponse)
    {
        var request = new IntegralResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<IntegralResponseRequest, int>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Theory]
    [InlineData(int.MinValue)]
    [InlineData(0)]
    [InlineData(int.MaxValue)]
    public async Task Request_ForResponse_OfNullableIntegralType_WithValue(int? expectedResponse)
    {
        var request = new NullableIntegralResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<NullableIntegralResponseRequest, int?>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Fact]
    public async Task Request_ForResponse_OfNullableIntegralType_NoValue()
    {
        var request = new NullableIntegralResponseRequest() { ExpectedResponse = null };
        var caller = GetCaller<NullableIntegralResponseRequest, int?>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }

    [Theory]
    [InlineData(float.MinValue)]
    [InlineData(0f)]
    [InlineData(float.MaxValue)]
    public async Task Request_ForResponse_OfFloatingType(float expectedResponse)
    {
        var request = new FloatingResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<FloatingResponseRequest, float>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Theory]
    [InlineData(float.MinValue)]
    [InlineData(0f)]
    [InlineData(float.MaxValue)]
    public async Task Request_ForResponse_OfNullableFloatingType_WithValue(float? expectedResponse)
    {
        var request = new NullableFloatingResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<NullableFloatingResponseRequest, float?>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Fact]
    public async Task Request_ForResponse_OfNullableFloatingType_NoValue()
    {
        var request = new NullableFloatingResponseRequest() { ExpectedResponse = null };
        var caller = GetCaller<NullableFloatingResponseRequest, float?>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task Request_ForResponse_OfBooleanType(bool expectedResponse)
    {
        var request = new BooleanResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<BooleanResponseRequest, bool>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task Request_ForResponse_OfNullableBooleanType_WithValue(bool? expectedResponse)
    {
        var request = new NullableBooleanResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<NullableBooleanResponseRequest, bool?>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Fact]
    public async Task Request_ForResponse_OfNullableBooleanType_NoValue()
    {
        var request = new NullableBooleanResponseRequest() { ExpectedResponse = null };
        var caller = GetCaller<NullableBooleanResponseRequest, bool?>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }

    [Theory]
    [InlineData('\0')]
    [InlineData('x')]
    [InlineData('\x006A')]
    public async Task Request_ForResponse_OfCharacterType(char expectedResponse)
    {
        var request = new CharacterResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<CharacterResponseRequest, char>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Theory]
    [InlineData('\0')]
    [InlineData('x')]
    [InlineData('\x006A')]
    public async Task Request_ForResponse_OfNullableCharacterType_WithValue(char? expectedResponse)
    {
        var request = new NullableCharacterResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<NullableCharacterResponseRequest, char?>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Fact]
    public async Task Request_ForResponse_OfNullableCharacterType_NoValue()
    {
        var request = new NullableCharacterResponseRequest() { ExpectedResponse = null };
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
