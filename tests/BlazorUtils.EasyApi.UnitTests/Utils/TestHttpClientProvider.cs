using BlazorUtils.EasyApi.Client.Setup;

namespace BlazorUtils.EasyApi.UnitTests.Utils;

internal class TestHttpClientProvider : IHttpClientProvider
{
    private readonly HttpClient _httpClient;

    public TestHttpClientProvider(TestResponseProvider responseProvider, OnSendCallback? onSend = null)
    {
        _httpClient = new HttpClient(new TestHttpMessageHandler(responseProvider, onSend))
        {
            BaseAddress = new("https://example.com")
        };
    }

    public HttpClient GetClient(IRequest request) => _httpClient;
}
