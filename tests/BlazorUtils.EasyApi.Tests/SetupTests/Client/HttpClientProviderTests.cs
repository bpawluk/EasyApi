using BlazorUtils.EasyApi.Client;
using BlazorUtils.EasyApi.Client.Setup;
using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Shared.Setup;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace BlazorUtils.EasyApi.Tests.SetupTests.Client;

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
            .WithContract(typeof(HttpClientProviderTestsRequest).Assembly)
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
        var caller = scope.ServiceProvider.GetRequiredService<ICall<HttpClientProviderTestsRequest>>();
        await caller.Call(new());
        Assert.Equal(3, _calls.Count);

        scope = _sut.CreateScope();
        caller = scope.ServiceProvider.GetRequiredService<ICall<HttpClientProviderTestsRequest>>();
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
        Initialize(appBuilder => appBuilder.Using(new TestHttpClientProvider(OnSend)));
        await CallApi(3);
        Assert.Single(_calls.Distinct());
    }

    private async Task CallApi(int times)
    {
        for (int currentCall = 1; currentCall <= times; currentCall++)
        {
            var caller = _sut.GetRequiredService<ICall<HttpClientProviderTestsRequest>>();
            await caller.Call(new());
            Assert.Equal(currentCall, _calls.Count);
        }
    }

    private void OnSend(Guid providerId) => _calls.Add(providerId);

    public void Dispose() => _sut.Dispose();
}

internal delegate void OnSendCallback(Guid providerId);

internal class TestHttpMessageHandler(OnSendCallback OnSend) : HttpMessageHandler
{
    private readonly Guid _id = Guid.NewGuid();

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        OnSend(_id);
        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
    }
}

internal class TestHttpClientProvider : IHttpClientProvider
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

internal class AuthorizationTestsRequestHandler: IHandle<HttpClientProviderTestsRequest>
{
    public Task<HttpResult> Handle(HttpClientProviderTestsRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult.Ok());
}

[Route(nameof(HttpClientProviderTestsRequest))]
public class HttpClientProviderTestsRequest : IGet { }
