using BlazorUtils.EasyApi.Client;

namespace BlazorUtils.EasyApi.Tests.Utils;

internal class HttpClientProvider(HttpClient httpClient) : IHttpClientProvider
{
    private readonly HttpClient _httpClient = httpClient;

    public HttpClient GetClient(IRequest _) => _httpClient;
}
