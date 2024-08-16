using BlazorUtils.EasyApi.Client;
using BlazorUtils.EasyApi.Client.Setup;
using BlazorUtils.EasyApi.UnitTests.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorUtils.EasyApi.UnitTests.SetupTests.Client;

public sealed class HttpClientProviderTests : IDisposable
{
    private readonly List<Guid> _calls = [];
    private ServiceProvider _sut = default!;

    private void Initialize(Action<ClientBuilder> additionalSetup)
    {
        var services = new ServiceCollection();

        services.AddSingleton(new TestResponseProvider());
        services.AddSingleton<OnSendCallback>(OnSend);

        var easyApiBuilder = services
            .AddEasyApi()
            .WithContract(GetType().Assembly)
            .WithClient();

        additionalSetup(easyApiBuilder);

        _sut = services.BuildServiceProvider();
    }

    [Fact]
    public async Task Provider_WithDefaultLifetime()
    {
        Initialize(appBuilder => appBuilder.Using<TestHttpClientProvider>());
        await CallApi(3);
        Assert.Equal(3, _calls.Distinct().Count());
    }

    [Fact]
    public async Task Provider_WithTransientLifetime()
    {
        Initialize(appBuilder => appBuilder.Using<TestHttpClientProvider>(ServiceLifetime.Transient));
        await CallApi(3);
        Assert.Equal(3, _calls.Distinct().Count());
    }

    [Fact]
    public async Task Provider_WithScopedLifetime()
    {
        Initialize(appBuilder => appBuilder.Using<TestHttpClientProvider>(ServiceLifetime.Scoped));

        await CallApi(2);

        var scope = _sut.CreateScope();
        var caller = scope.ServiceProvider.GetRequiredService<ICall<HttpClientProviderTestsRequest, string>>();
        await caller.Call(new());
        Assert.Equal(3, _calls.Count);

        scope = _sut.CreateScope();
        caller = scope.ServiceProvider.GetRequiredService<ICall<HttpClientProviderTestsRequest, string>>();
        await caller.Call(new());
        Assert.Equal(4, _calls.Count);

        Assert.Equal(3, _calls.Distinct().Count());
    }

    [Fact]
    public async Task Provider_WithSingletonLifetime()
    {
        Initialize(appBuilder => appBuilder.Using<TestHttpClientProvider>(ServiceLifetime.Singleton));
        await CallApi(3);
        Assert.Single(_calls.Distinct());
    }

    [Fact]
    public async Task Provider_WithSingletonInstance()
    {
        Initialize(appBuilder => appBuilder.Using(new TestHttpClientProvider(new TestResponseProvider(), OnSend)));
        await CallApi(3);
        Assert.Single(_calls.Distinct());
    }

    private async Task CallApi(int times)
    {
        for (int currentCall = 1; currentCall <= times; currentCall++)
        {
            var caller = _sut.GetRequiredService<ICall<HttpClientProviderTestsRequest, string>>();
            await caller.Call(new());
            Assert.Equal(currentCall, _calls.Count);
        }
    }

    private void OnSend(Guid providerId) => _calls.Add(providerId);

    public void Dispose() => _sut.Dispose();
}

[Route(nameof(HttpClientProviderTestsRequest))]
public class HttpClientProviderTestsRequest : IGet<string> { }

internal class HttpClientProviderTestsRequestHandler(TestResponseProvider responseProvider) : TestRequestHandler<HttpClientProviderTestsRequest>(responseProvider) { }
