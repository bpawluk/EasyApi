﻿using BlazorUtils.EasyApi.Client.Json;
using BlazorUtils.EasyApi.Client.Setup;
using BlazorUtils.EasyApi.Shared.Contract;
using BlazorUtils.EasyApi.Shared.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace BlazorUtils.EasyApi.Client.Http;

internal abstract class HttpCallerBase<Request>
    where Request : class, IRequest, new()
{
    protected readonly JsonOptionsProvider _jsonOptions;

    private readonly IHttpClientProvider _httpClientProvider;
    private readonly RequestAccessor<Request> _accessor;
    private readonly JsonSerializerOptions _bodyParamsJsonSerializerOptions;

    protected HttpCallerBase(IHttpClientProvider httpClientProvider, Requests requests, JsonOptionsProvider jsonOptions)
    {
        _httpClientProvider = httpClientProvider;
        _accessor = requests.Get<Request>();
        _jsonOptions = jsonOptions;
        _bodyParamsJsonSerializerOptions = _jsonOptions.Create();
        _bodyParamsJsonSerializerOptions.Converters.Add(new RequestBodyConverter<Request>(_accessor));
    }

    protected HttpRequestMessage GetHttpRequest(Request request, HttpMethod method)
    {
        var uri = GetUri(request);
        var content = method.CanHaveContent() ? WriteContent(request) : default;
        var httpRequest = new HttpRequestMessage(method, uri) { Content = content };
        foreach (var param in _accessor.HeaderParams)
        {
            var paramValue = param.ReadFrom(request);
            if (paramValue != null)
            {
                httpRequest.Headers.Add(param.Name, paramValue);
            }
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
        var route = _accessor.RouteInfo.Value;

        foreach (var param in _accessor.RouteParams)
        {
            var paramValue = HttpUtility.UrlEncode(param.ReadFrom(request));
            route = route.Replace($"{{{param.Name}}}", paramValue);
        }

        var queryString = HttpUtility.ParseQueryString(string.Empty);
        foreach (var param in _accessor.QueryStringParams)
        {
            var paramValue = param.ReadFrom(request);
            if (paramValue != null)
            {
                queryString[param.Name] = paramValue;
            }
        }
        route = queryString.Count > 0 ? $"{route}?{queryString}" : route;

        return new(route, UriKind.Relative);
    }

    private JsonContent? WriteContent(Request request)
    {
        if (_accessor.BodyParams.Any())
        {
            return JsonContent.Create(request, options: _bodyParamsJsonSerializerOptions);
        }
        return null;
    }
}
