using BlazorUtils.EasyApi.IntegrationTests.SUT.Client;
using BlazorUtils.EasyApi.IntegrationTests.SUT.Server;
using BlazorUtils.EasyApi.IntegrationTests.SUT.Server.Auth;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace BlazorUtils.EasyApi.IntegrationTests;

public abstract class TestsBase : IClassFixture<TestsFixture>
{
    protected readonly App _client;
    protected readonly WebApplicationFactory<Program> _server;

    public TestsBase(TestsFixture fixture)
    {
        _client = fixture.Client;
        _server = fixture.Server;
        SignOut();
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

    protected void SignIn(string username)
    {
        _server.Services.GetRequiredService<TestAuthenticationSettings>().UserName = username;
    }

    protected void SignOut()
    {
        _server.Services.GetRequiredService<TestAuthenticationSettings>().UserName = null;
    }
}