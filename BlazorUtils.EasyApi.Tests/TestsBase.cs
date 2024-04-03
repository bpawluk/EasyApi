using BlazorUtils.EasyApi.Tests.SUT.Client;
using BlazorUtils.EasyApi.Tests.SUT.Server;
using BlazorUtils.EasyApi.Tests.Utils;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace BlazorUtils.EasyApi.Tests;

public abstract class TestsBase : IClassFixture<WebApplicationFactory<Program>>, IDisposable
{
    protected readonly App _client;
    protected readonly WebApplicationFactory<Program> _server;

    public TestsBase(WebApplicationFactory<Program> factory)
    {
        var httpClient = factory.CreateClient();
        var httpClientProvider = new HttpClientProvider(httpClient);
        _client = App.Create(httpClientProvider);
        _server = factory;
    }

    protected abstract ICall<Request> GetCaller<Request>() 
        where Request : class, IRequest, new();

    protected abstract ICall<Request, Response> GetCaller<Request, Response>() 
        where Request : class, IRequest<Response>, new();

    protected async Task CallHttp<Request>(Request request, HttpStatusCode expectedStatusCode = HttpStatusCode.OK) 
        where Request : class, IRequest, new()
    {
        var caller = GetCaller<Request>();
        var response = await caller.CallHttp(request, CancellationToken.None);
        Assert.NotNull(response);
        Assert.Equal(expectedStatusCode, response.StatusCode);
    }

    protected async Task<Response> CallHttp<Request, Response>(Request request, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        where Request : class, IRequest<Response>, new()
    {
        var caller = GetCaller<Request, Response>();
        var response = await caller.CallHttp(request, CancellationToken.None);
        Assert.NotNull(response);
        Assert.Equal(expectedStatusCode, response.StatusCode);
        Assert.True(response.HasResponse);
        return response.Response!;
    }

    public void Dispose()
    {
        _client.Dispose();
        GC.SuppressFinalize(this);
    }
}