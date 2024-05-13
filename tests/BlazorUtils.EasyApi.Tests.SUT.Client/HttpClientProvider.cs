using BlazorUtils.EasyApi.Client.Setup;

namespace BlazorUtils.EasyApi.Tests.SUT.Client;

public class HttpClientProvider(HttpClient httpClient) : IHttpClientProvider
{
    private readonly HttpClient _httpClient = httpClient;

    public HttpClient GetClient(IRequest _) => _httpClient;
}
