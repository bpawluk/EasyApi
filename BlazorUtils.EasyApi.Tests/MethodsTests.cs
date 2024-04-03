using BlazorUtils.EasyApi.Tests.SUT.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorUtils.EasyApi.Tests;

public abstract class MethodsTests(TestsFixture fixture) : TestsBase(fixture)
{
    [Fact]
    public async Task Request_HttpGet()
    {
        var request = new GetRequest() { Id = Guid.NewGuid() };
        await CallHttp(request);
    }

    [Fact]
    public async Task Request_HttpGet_WithResponse()
    {
        var request = new GetRequestWithResponse() { Id = Guid.NewGuid() };
        var response = await CallHttp<GetRequestWithResponse, MethodRequestResponse>(request);
        Assert.Equal(request.Id, response.Id);
    }

    [Fact]
    public async Task Request_HttpHead()
    {
        var request = new HeadRequest() { Id = Guid.NewGuid() };
        await CallHttp(request);
    }

    [Fact]
    public async Task Request_HttpPatch()
    {
        var request = new PatchRequest() { Id = Guid.NewGuid() };
        await CallHttp(request);
    }

    [Fact]
    public async Task Request_HttpPatch_WithResponse()
    {
        var request = new PatchRequestWithResponse() { Id = Guid.NewGuid() };
        var response = await CallHttp<PatchRequestWithResponse, MethodRequestResponse>(request);
        Assert.Equal(request.Id, response.Id);
    }

    [Fact]
    public async Task Request_HttpPost()
    {
        var request = new PostRequest() { Id = Guid.NewGuid() };
        await CallHttp(request);
    }

    [Fact]
    public async Task Request_HttpPost_WithResponse()
    {
        var request = new PostRequestWithResponse() { Id = Guid.NewGuid() };
        var response = await CallHttp<PostRequestWithResponse, MethodRequestResponse>(request);
        Assert.Equal(request.Id, response.Id);
    }

    [Fact]
    public async Task Request_HttpPut()
    {
        var request = new PutRequest() { Id = Guid.NewGuid() };
        await CallHttp(request);
    }

    [Fact]
    public async Task Request_HttpPut_WithResponse()
    {
        var request = new PutRequestWithResponse() { Id = Guid.NewGuid() };
        var response = await CallHttp<PutRequestWithResponse, MethodRequestResponse>(request);
        Assert.Equal(request.Id, response.Id);
    }

    [Fact]
    public async Task Request_HttpDelete()
    {
        var request = new DeleteRequest() { Id = Guid.NewGuid() };
        await CallHttp(request);
    }

    [Fact]
    public async Task Request_HttpDelete_WithResponse()
    {
        var request = new DeleteRequestWithResponse() { Id = Guid.NewGuid() };
        var response = await CallHttp<DeleteRequestWithResponse, MethodRequestResponse>(request);
        Assert.Equal(request.Id, response.Id);
    }
}

public class Client_MethodsTests(TestsFixture fixture) : MethodsTests(fixture)
{
    protected override ICall<Request> GetCaller<Request>() => _client.Services.GetRequiredService<ICall<Request>>();

    protected override ICall<Request, Response> GetCaller<Request, Response>() => _client.Services.GetRequiredService<ICall<Request, Response>>();
}

public class Server_MethodsTests(TestsFixture fixture) : MethodsTests(fixture)
{
    protected override ICall<Request> GetCaller<Request>() => _server.Services.GetRequiredService<ICall<Request>>();

    protected override ICall<Request, Response> GetCaller<Request, Response>() => _server.Services.GetRequiredService<ICall<Request, Response>>();
}
