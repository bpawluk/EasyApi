using BlazorUtils.EasyApi.Client;

namespace BlazorUtils.EasyApi.Benchmarks.SUT.Client;

public class HttpClientProvider(HttpClient httpClient) : IHttpClientProvider
{
    private readonly HttpClient _httpClient = httpClient;

    public HttpClient GetClient(IRequest _) => _httpClient;
}
