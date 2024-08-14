using BlazorUtils.EasyApi.Client;
using BlazorUtils.EasyApi.Client.Setup;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http.Json;

namespace BlazorUtils.EasyApi.UnitTests.PersistenceTests.InMemory.Client;

public class InMemoryResponseStoreTests : InMemoryResponseStoreTestsBase
{
    public InMemoryResponseStoreTests()
    {
        Services
            .AddEasyApi()
            .WithContract(GetType().Assembly)
            .WithClient()
            .Using<TestHttpClientProvider>()
            .Using<InMemoryResponsePersistence>();

        Services.Replace(ServiceDescriptor.Singleton(_interactivityDetectorMock.Object));
    }
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