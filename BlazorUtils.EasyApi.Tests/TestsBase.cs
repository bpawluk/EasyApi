using BlazorUtils.EasyApi.Tests.SUT.Client;
using BlazorUtils.EasyApi.Tests.SUT.Server;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace BlazorUtils.EasyApi.Tests;

public abstract class TestsBase(TestsFixture fixture) : IClassFixture<TestsFixture>
{
    protected readonly App _client = fixture.Client;
    protected readonly WebApplicationFactory<Program> _server = fixture.Server;

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
}