using BlazorUtils.EasyApi.Client.Setup;
using BlazorUtils.EasyApi.Shared.Contract;
using BlazorUtils.EasyApi.Shared.Json;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorUtils.EasyApi.Client.Http;

internal class HttpCaller<Request, Response>(IHttpClientProvider httpClientProvider, Requests requests) 
    : HttpCallerBase<Request>(httpClientProvider, requests)
    , ICall<Request, Response>
    where Request : class, IRequest<Response>, new()
{
    public Task<Response> Call(Request request) => Call(request, CancellationToken.None);

    public async Task<Response> Call(Request request, CancellationToken cancellationToken)
    {
        var httpResult = await CallHttp(request, cancellationToken).ConfigureAwait(false);
        httpResult.EnsureSucceeded();
        return httpResult.Response!;
    }

    public Task<HttpResult<Response>> CallHttp(Request request) => CallHttp(request, CancellationToken.None);

    public async Task<HttpResult<Response>> CallHttp(Request request, CancellationToken cancellationToken)
    {
        var method = request.GetHttpMethod<Request, Response>();
        var httpRequest = GetHttpRequest(request, method);
        var httpResponse = await GetHttpResponse(request, httpRequest, cancellationToken).ConfigureAwait(false);
        var contentReadingResult = await TryReadContent(httpResponse.Content, cancellationToken).ConfigureAwait(false);
        if (contentReadingResult.HasResponse)
        {
            return HttpResult<Response>.WithStatusCode(httpResponse.StatusCode, contentReadingResult.Response!);
        }
        return HttpResult<Response>.WithStatusCode(httpResponse.StatusCode);
    }

    private static async Task<(bool HasResponse, Response? Response)> TryReadContent(HttpContent content, CancellationToken cancellationToken)
    {
        var body = await content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
        if (body != Stream.Null && body.CanRead)
        {
            try
            {
                var response = await JsonSerializer.DeserializeAsync<Response>(body, JsonOptions.Get, cancellationToken).ConfigureAwait(false);
                return (true, response);
            }
            catch (JsonException) { }
        }
        return (false, default);
    }
}
