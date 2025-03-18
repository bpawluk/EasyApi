using BlazorUtils.EasyApi.Client.Setup;
using BlazorUtils.EasyApi.Shared.Contract;
using BlazorUtils.EasyApi.Shared.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorUtils.EasyApi.Client.Http;

internal class HttpCaller<Request>(IHttpClientProvider httpClientProvider, Requests requests, JsonOptionsProvider jsonOptions) 
    : HttpCallerBase<Request>(httpClientProvider, requests, jsonOptions)
    , ICall<Request>
    where Request : class, IRequest, new()
{
    public Task Call(Request request) => Call(request, CancellationToken.None);

    public async Task Call(Request request, CancellationToken cancellationToken)
    {
        var httpResult = await CallHttp(request, cancellationToken).ConfigureAwait(false);
        httpResult.EnsureSucceeded();
    }

    public Task<HttpResult> CallHttp(Request request) => CallHttp(request, CancellationToken.None);

    public async Task<HttpResult> CallHttp(Request request, CancellationToken cancellationToken)
    {
        var method = request.GetHttpMethod();
        var httpRequest = GetHttpRequest(request, method);
        var httpResponse = await GetHttpResponse(request, httpRequest, cancellationToken).ConfigureAwait(false);
        return HttpResult.WithStatusCode(httpResponse.StatusCode);
    }
}
