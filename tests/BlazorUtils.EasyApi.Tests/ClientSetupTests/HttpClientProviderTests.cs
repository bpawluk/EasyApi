using BlazorUtils.EasyApi.Client;
using BlazorUtils.EasyApi.Client.Setup;
using BlazorUtils.EasyApi.Shared.Setup;
using BlazorUtils.EasyApi.Tests.SUT.Contract;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace BlazorUtils.EasyApi.Tests.ClientSetupTests;

public sealed class HttpClientProviderTests : IDisposable
{
    private readonly List<Guid> _calls = [];
    private ServiceProvider _sut = default!;

    private void Initialize(Action<AppBuilder> additionalSetup)
    {
        var services = new ServiceCollection();
        services.AddSingleton<OnSendCallback>(OnSend);
        var easyApiBuilder = services
            .AddEasyApi()
            .WithContract(typeof(EmptyGet).Assembly)
            .WithClient();
        additionalSetup(easyApiBuilder);
        _sut = services.BuildServiceProvider();
    }

    [Fact]
    public async Task HttpClientProvider_WithDefaultLifetime()
    {
        Initialize(appBuilder => appBuilder.UsingHttpClientProvider<TestHttpClientProvider>());
        await CallApi(3);
        Assert.Equal(3, _calls.Distinct().Count());
    }

    [Fact]
    public async Task HttpClientProvider_WithTransientLifetime()
    {
        Initialize(appBuilder => appBuilder.UsingHttpClientProvider<TestHttpClientProvider>(ServiceLifetime.Transient));
        await CallApi(3);
        Assert.Equal(3, _calls.Distinct().Count());
    }

    [Fact]
    public async Task HttpClientProvider_WithScopedLifetime()
    {
        Initialize(appBuilder => appBuilder.UsingHttpClientProvider<TestHttpClientProvider>(ServiceLifetime.Scoped));

        await CallApi(2);

        var scope = _sut.CreateScope();
        var caller = scope.ServiceProvider.GetRequiredService<ICall<EmptyGet>>();
        await caller.Call(new());
        Assert.Equal(3, _calls.Count);

        scope = _sut.CreateScope();
        caller = scope.ServiceProvider.GetRequiredService<ICall<EmptyGet>>();
        await caller.Call(new());
        Assert.Equal(4, _calls.Count);

        Assert.Equal(3, _calls.Distinct().Count());
    }

    [Fact]
    public async Task HttpClientProvider_WithSingletonLifetime()
    {
        Initialize(appBuilder => appBuilder.UsingHttpClientProvider<TestHttpClientProvider>(ServiceLifetime.Singleton));
        await CallApi(3);
        Assert.Single(_calls.Distinct());
    }

    [Fact]
    public async Task HttpClientProvider_WithSingletonInstance()
    {
        Initialize(appBuilder => appBuilder.UsingHttpClientProvider(new TestHttpClientProvider(OnSend)));
        await CallApi(3);
        Assert.Single(_calls.Distinct());
    }

    private async Task CallApi(int times)
    {
        for (int currentCall = 1; currentCall <= times; currentCall++)
        {
            var caller = _sut.GetRequiredService<ICall<EmptyGet>>();
            await caller.Call(new());
            Assert.Equal(currentCall, _calls.Count);
        }
    }

    private void OnSend(Guid providerId) => _calls.Add(providerId);

    private class TestHttpClientProvider : IHttpClientProvider
    {
        private readonly HttpClient _httpClient;

        public TestHttpClientProvider(OnSendCallback onSend)
        {
            _httpClient = new HttpClient(new TestHttpMessageHandler(onSend))
            {
                BaseAddress = new("https://example.com")
            };
        }

        public HttpClient GetClient(IRequest request) => _httpClient;
    }

    private class TestHttpMessageHandler(OnSendCallback OnSend) : HttpMessageHandler
    {
        private readonly Guid _id = Guid.NewGuid();

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            OnSend(_id);
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        }
    }

    private delegate void OnSendCallback(Guid providerId);

    public void Dispose() => _sut.Dispose();
}
