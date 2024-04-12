using BlazorUtils.EasyApi.Tests.SUT.Contract.ResponseTypes;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace BlazorUtils.EasyApi.Tests.ResponseTests.ResponseTypesTests;

public abstract class EnumResponseTests(TestsFixture fixture) : TestsBase(fixture)
{
    [Fact]
    public async Task Request_ForResponse_OfEnumType()
    {
        var request = new EnumResponseRequest() { ExpectedResponse = Time.Day };
        var result = await CallHttp<EnumResponseRequest, Time>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Fact]
    public async Task Request_ForResponse_OfNullableEnumType_WithValue()
    {
        var request = new NullableEnumResponseRequest() { ExpectedResponse = Time.Night };
        var result = await CallHttp<NullableEnumResponseRequest, Time?>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Fact]
    public async Task Request_ForResponse_OfNullableEnumType_NoValue()
    {
        var request = new NullableEnumResponseRequest() { ExpectedResponse = null };
        var caller = GetCaller<NullableEnumResponseRequest, Time?>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }
}

public class Client_EnumResponseTests(TestsFixture fixture) : EnumResponseTests(fixture)
{
    protected override ICall<Request> GetCaller<Request>() => _client.Services.GetRequiredService<ICall<Request>>();

    protected override ICall<Request, Response> GetCaller<Request, Response>() => _client.Services.GetRequiredService<ICall<Request, Response>>();
}

public class Server_EnumResponseTests(TestsFixture fixture) : EnumResponseTests(fixture)
{
    protected override ICall<Request> GetCaller<Request>() => _server.Services.GetRequiredService<ICall<Request>>();

    protected override ICall<Request, Response> GetCaller<Request, Response>() => _server.Services.GetRequiredService<ICall<Request, Response>>();
}
