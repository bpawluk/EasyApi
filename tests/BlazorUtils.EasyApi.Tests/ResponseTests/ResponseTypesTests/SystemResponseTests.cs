using BlazorUtils.EasyApi.Tests.SUT.Contract.Response;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace BlazorUtils.EasyApi.Tests.ResponseTests.ResponseTypesTests;

public abstract class SystemResponseTests(TestsFixture fixture) : TestsBase(fixture)
{
    [Fact]
    public async Task Request_ForResponse_OfGuidType()
    {
        var request = new GuidResponseRequest() { ExpectedResponse = Guid.NewGuid() };
        var result = await CallHttp<GuidResponseRequest, Guid>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Fact]
    public async Task Request_ForResponse_OfNullableGuidType_WithValue()
    {
        var request = new NullableGuidResponseRequest() { ExpectedResponse = Guid.NewGuid() };
        var result = await CallHttp<NullableGuidResponseRequest, Guid?>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Fact]
    public async Task Request_ForResponse_OfNullableGuidType_NoValue()
    {
        var request = new NullableGuidResponseRequest() { ExpectedResponse = null };
        var caller = GetCaller<NullableGuidResponseRequest, Guid?>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("text")]
    public async Task Request_ForResponse_OfStringType_WithValue(string expectedResponse)
    {
        var request = new StringResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<StringResponseRequest, string>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Request_ForResponse_OfStringType_NoValue(string? expectedResponse)
    {
        var request = new StringResponseRequest() { ExpectedResponse = expectedResponse! };
        var caller = GetCaller<StringResponseRequest, string>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }

    [Fact]
    public async Task Request_ForResponse_OfUriType_WithValue()
    {
        var request = new UriResponseRequest() { ExpectedResponse = new Uri("http://www.example.com") };
        var result = await CallHttp<UriResponseRequest, Uri>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Fact]
    public async Task Request_ForResponse_OfUriType_NoValue()
    {
        var request = new UriResponseRequest() { ExpectedResponse = null! };
        var caller = GetCaller<UriResponseRequest, Uri>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }
}

public class Client_SystemResponseTests(TestsFixture fixture) : SystemResponseTests(fixture)
{
    protected override ICall<Request> GetCaller<Request>() => _client.Services.GetRequiredService<ICall<Request>>();

    protected override ICall<Request, Response> GetCaller<Request, Response>() => _client.Services.GetRequiredService<ICall<Request, Response>>();
}

public class Server_SystemResponseTests(TestsFixture fixture) : SystemResponseTests(fixture)
{
    protected override ICall<Request> GetCaller<Request>() => _server.Services.GetRequiredService<ICall<Request>>();

    protected override ICall<Request, Response> GetCaller<Request, Response>() => _server.Services.GetRequiredService<ICall<Request, Response>>();
}
