using BlazorUtils.EasyApi.Tests.SUT.Client;
using BlazorUtils.EasyApi.Tests.SUT.Server;
using BlazorUtils.EasyApi.Tests.Utils;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BlazorUtils.EasyApi.Tests;

public sealed class TestsFixture : IDisposable
{
    public App Client { get; }
    public WebApplicationFactory<Program> Server { get; }

    public TestsFixture()
    {
        var factory = new WebApplicationFactory<Program>();
        var httpClient = factory.CreateClient();
        var httpClientProvider = new HttpClientProvider(httpClient);
        Client = App.Create(httpClientProvider);
        Server = factory;
    }

    public void Dispose()
    {
        Client.Dispose();
        Server.Dispose();
    }
}
