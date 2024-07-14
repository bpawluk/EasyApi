using BlazorUtils.EasyApi.Client;
using BlazorUtils.EasyApi.Client.Setup;
using System.Net.Http.Json;

namespace BlazorUtils.EasyApi.UnitTests.PrerenderingTests.Client;

public class PrerenderedResponseStoreTests : PrerenderedResponseStoreTestsBase
{
    public PrerenderedResponseStoreTests()
    {
        Services
            .AddEasyApi()
            .WithContract(GetType().Assembly)
            .WithClient()
            .Using<TestHttpClientProvider>()
            .Using<PrerenderedResponsePersistence>();
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