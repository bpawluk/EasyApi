using BlazorUtils.EasyApi.Tests.SUT.Client;
using BlazorUtils.EasyApi.Tests.SUT.Server;
using BlazorUtils.EasyApi.Tests.Utils;
using Microsoft.AspNetCore.Mvc.Testing;

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

    public void Dispose()
    {
        _sut.Dispose();
        GC.SuppressFinalize(this);
    }
}