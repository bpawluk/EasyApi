using BlazorUtils.EasyApi.Tests.SUT.Client;
using BlazorUtils.EasyApi.Tests.SUT.Server;
using BlazorUtils.EasyApi.Tests.Utils;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace BlazorUtils.EasyApi.Tests;

public abstract class TestsBase : IClassFixture<WebApplicationFactory<Program>>, IDisposable
{
    protected readonly App _sut;

    public TestsBase(WebApplicationFactory<Program> factory)
    {
        var httpClient = factory.CreateClient();
        var httpClientProvider = new HttpClientProvider(httpClient);
        _sut = App.Create(httpClientProvider);
    }

    protected async Task CallHttp<Request>(Request request, HttpStatusCode expectedStatusCode = HttpStatusCode.OK) 
        where Request : class, IRequest, new()
    {
        var caller = _sut.Services.GetRequiredService<ICall<Request>>();
        var response = await caller.CallHttp(request, CancellationToken.None);
        Assert.NotNull(response);
        Assert.Equal(expectedStatusCode, response.StatusCode);
    }

    protected async Task<Response> CallHttp<Request, Response>(Request request, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        where Request : class, IRequest<Response>, new()
    {
        var caller = _sut.Services.GetRequiredService<ICall<Request, Response>>();
        var response = await caller.CallHttp(request, CancellationToken.None);
        Assert.NotNull(response);
        Assert.Equal(expectedStatusCode, response.StatusCode);
        Assert.True(response.HasResponse);
        return response.Response!;
    }

    public void Dispose()
    {
        _sut.Dispose();
        GC.SuppressFinalize(this);
    }
}