using BlazorUtils.EasyApi.Client.Json;
using BlazorUtils.EasyApi.Shared.Contract;
using System;
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
        var method = request.GetHttpMethod();
        var httpRequest = GetHttpRequest(request, method);
        await GetHttpResponse(request, httpRequest, cancellationToken).ConfigureAwait(false);
    }
}

internal class HttpCaller<Request, Response> : HttpCallerBase<Request>, ICall<Request, Response>
    where Request : class, IRequest<Response>, new()
{
    public HttpCaller(IHttpClientProvider httpClientProvider, Requests requests) 
        : base(httpClientProvider, requests) { }

    public async Task<Response> Call(Request request, CancellationToken cancellationToken)
    {
        var method = request.GetHttpMethod<Request, Response>();
        var httpRequest = GetHttpRequest(request, method);
        var httpResponse = await GetHttpResponse(request, httpRequest, cancellationToken).ConfigureAwait(false);
        return (await httpResponse.Content.ReadFromJsonAsync<Response>(cancellationToken: cancellationToken).ConfigureAwait(false))!;
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
        _jsonSerializerOptions = new JsonSerializerOptions();
        _jsonSerializerOptions.Converters.Add(new RequestBodyConverter<Request>(_accessor));
    }

    protected HttpRequestMessage GetHttpRequest(Request request, HttpMethod method)
    {
        var uri = GetUri(request);
        var content = method.CanHaveContent() ? GetContent(request) : default;
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
        var httpResponse = await client.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);
        return httpResponse.EnsureSuccessStatusCode();
    }

    private Uri GetUri(Request request)
    {
        var route = _accessor.GetRoute();

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

    private JsonContent? GetContent(Request request) => JsonContent.Create(request, options: _jsonSerializerOptions);
}
