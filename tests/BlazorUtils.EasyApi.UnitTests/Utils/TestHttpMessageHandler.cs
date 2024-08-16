using System.Net.Http.Json;

namespace BlazorUtils.EasyApi.UnitTests.Utils;

internal delegate void OnSendCallback(Guid handlerId);

internal class TestHttpMessageHandler(TestResponseProvider responseProvider, OnSendCallback? onSend) : HttpMessageHandler
{
    private readonly Guid _id = Guid.NewGuid();
    private readonly TestResponseProvider _responseProvider = responseProvider;
    private readonly OnSendCallback? _onSend = onSend;

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        _onSend?.Invoke(_id);
        var response = _responseProvider.Response;
        return Task.FromResult(new HttpResponseMessage(response.StatusCode)
        {
            Content = response.Response is null ? null : JsonContent.Create(response.Response!)
        });
    }
}
