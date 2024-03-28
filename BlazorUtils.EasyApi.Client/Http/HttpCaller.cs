using BlazorUtils.EasyApi.Client.Json;
using BlazorUtils.EasyApi.Shared.Contract;
using BlazorUtils.EasyApi.Shared.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace BlazorUtils.EasyApi.Client.Http;

internal class HttpCaller<Request> : HttpCallerBase<Request>, ICall<Request>
    where Request : class, IRequest, new()
{
    public HttpCaller(IHttpClientProvider httpClientProvider, Requests requests)
        : base(httpClientProvider, requests) { }

    public async Task Call(Request request, CancellationToken cancellationToken)
    {
        var httpResult = await CallHttp(request, cancellationToken);
        httpResult.EnsureSucceeded();
    }

    public async Task<HttpResult> CallHttp(Request request, CancellationToken cancellationToken)
    {
        var method = request.GetHttpMethod();
        var httpRequest = GetHttpRequest(request, method);
        var httpResponse = await GetHttpResponse(request, httpRequest, cancellationToken).ConfigureAwait(false);
        return HttpResult.WithStatusCode(httpResponse.StatusCode);
    }
}

internal class HttpCaller<Request, Response> : HttpCallerBase<Request>, ICall<Request, Response>
    where Request : class, IRequest<Response>, new()
{
    public HttpCaller(IHttpClientProvider httpClientProvider, Requests requests)
        : base(httpClientProvider, requests) { }

    public async Task<Response> Call(Request request, CancellationToken cancellationToken)
    {
        var httpResult = await CallHttp(request, cancellationToken);
        httpResult.EnsureSucceeded();
        return httpResult.Response!;
    }

    public async Task<HttpResult<Response>> CallHttp(Request request, CancellationToken cancellationToken)
    {
        var method = request.GetHttpMethod<Request, Response>();
        var httpRequest = GetHttpRequest(request, method);
        var httpResponse = await GetHttpResponse(request, httpRequest, cancellationToken).ConfigureAwait(false);
        var contentReadingResult = await TryReadContent(httpResponse.Content, cancellationToken);
        if (contentReadingResult.HasResponse)
        {
            return HttpResult<Response>.WithStatusCode(httpResponse.StatusCode, contentReadingResult.Response!);
        }
        return HttpResult<Response>.WithStatusCode(httpResponse.StatusCode);
    }

    private static async Task<(bool HasResponse, Response? Response)> TryReadContent(HttpContent content, CancellationToken cancellationToken)
    {
        var body = await content.ReadAsStreamAsync(cancellationToken);
        if (body != Stream.Null && body.CanRead)
        {
            var response = await JsonSerializer.DeserializeAsync<Response>(body, JsonOptions.Get, cancellationToken);
            return (true, response);
        }
        return (false, default);
    }
}

internal abstract class HttpCallerBase<Request>
    where Request : class, IRequest, new()
{
    private readonly IHttpClientProvider _httpClientProvider;
    private readonly RequestAccessor<Request> _accessor;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    protected HttpCallerBase(IHttpClientProvider httpClientProvider, Requests requests)
    {
        _httpClientProvider = httpClientProvider;
        _accessor = requests.Get<Request>();
        _jsonSerializerOptions = JsonOptions.Create();
        _jsonSerializerOptions.Converters.Add(new RequestBodyConverter<Request>(_accessor));
    }

    protected HttpRequestMessage GetHttpRequest(Request request, HttpMethod method)
    {
        var uri = GetUri(request);
        var content = method.CanHaveContent() ? WriteContent(request) : default;
        var httpRequest = new HttpRequestMessage(method, uri) { Content = content };
        foreach (var param in _accessor.GetHeaderParams(request))
        {
            httpRequest.Headers.Add(param.Name, param.ReadFrom(request));
        }
        return httpRequest;
    }

    protected async Task<HttpResponseMessage> GetHttpResponse(Request request, HttpRequestMessage httpRequest, CancellationToken cancellationToken)
    {
        var client = _httpClientProvider.GetClient(request);
        return await client.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);
    }

    private Uri GetUri(Request request)
    {
        var route = _accessor.Route;

        foreach (var param in _accessor.GetRouteParams(request))
        {
            var paramValue = HttpUtility.UrlEncode(param.ReadFrom(request));
            route = route.Replace($"{{{param.Name}}}", paramValue);
        }

        var queryString = HttpUtility.ParseQueryString(string.Empty);
        foreach (var param in _accessor.GetQueryStringParams(request))
        {
            queryString[param.Name] = param.ReadFrom(request);
        }
        route = queryString.Count > 0 ? $"{route}?{queryString}" : route;

        return new(route, UriKind.Relative);
    }

    private JsonContent WriteContent(Request request) => JsonContent.Create(request, options: _jsonSerializerOptions);
}
