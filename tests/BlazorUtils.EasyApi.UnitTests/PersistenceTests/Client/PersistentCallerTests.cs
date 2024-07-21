using BlazorUtils.EasyApi.Client;
using BlazorUtils.EasyApi.Client.Setup;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;

namespace BlazorUtils.EasyApi.UnitTests.PersistenceTests.Client;

public class PersistentCallerTests : PersistentCallerTestsBase
{
    protected override Task<IServiceProvider> CreateSUT(Action<IServiceCollection> servicesOverride)
    {
        var services = new ServiceCollection();

        services
            .AddEasyApi()
            .WithContract(GetType().Assembly)
            .WithClient()
            .Using<TestHttpClientProvider>()
            .Using<PrerenderedResponsePersistence>();

        servicesOverride(services);

        return Task.FromResult<IServiceProvider>(services.BuildServiceProvider());
    }

    public override Task DisposeAsync() => Task.CompletedTask;
}

internal class TestHttpMessageHandler(InnerCallerResponseProvider responseProvider) : HttpMessageHandler
{
    private readonly InnerCallerResponseProvider _innerCallerResponseProvider = responseProvider;

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = _innerCallerResponseProvider.Response;
        return Task.FromResult(new HttpResponseMessage(response.StatusCode)
        {
            Content = response.Response is null ? null : JsonContent.Create(response.Response!)
        });
    }
}

internal class TestHttpClientProvider : IHttpClientProvider
{
    private readonly HttpClient _httpClient;

    public TestHttpClientProvider(InnerCallerResponseProvider responseProvider)
    {
        _httpClient = new HttpClient(new TestHttpMessageHandler(responseProvider))
        {
            BaseAddress = new("https://example.com")
        };
    }

    public HttpClient GetClient(IRequest request) => _httpClient;
}