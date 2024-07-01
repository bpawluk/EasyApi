using BlazorUtils.EasyApi.Shared.Contract;
using BlazorUtils.EasyApi.Shared.Json;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorUtils.EasyApi.Server.Http;

internal static class HttpHandler
{
    public async static Task<IResult> Handle<Request>(
        HttpRequest httpRequest,
        Requests requests,
        IHandle<Request> handler,
        CancellationToken cancellationToken)
        where Request : class, IRequest, new()
    {
        var accessor = requests.Get<Request>();
        var request = await GetRequest(httpRequest, accessor, cancellationToken).ConfigureAwait(false);
        var result = await handler.Handle(request, cancellationToken).ConfigureAwait(false);
        return Results.StatusCode((int)result.StatusCode);
    }

    public async static Task<IResult> Handle<Request, Response>(
        HttpRequest httpRequest,
        Requests requests,
        IHandle<Request, Response> handler,
        CancellationToken cancellationToken)
        where Request : class, IRequest<Response>, new()
    {
        var accessor = requests.Get<Request>();
        var request = await GetRequest(httpRequest, accessor, cancellationToken).ConfigureAwait(false);
        var result = await handler.Handle(request, cancellationToken).ConfigureAwait(false);
        if (result.HasResponse)
        {
            return Results.Json(result.Response, options: JsonOptions.Get, statusCode: (int)result.StatusCode);
        }
        return Results.StatusCode((int)result.StatusCode);
    }

    private async static Task<Request> GetRequest<Request>(
        HttpRequest httpRequest,
        RequestAccessor<Request> accessor,
        CancellationToken cancellationToken) 
        where Request : class, IRequest, new()
    {
        var request = new Request();

        foreach (var param in accessor.RouteParams)
        {
            param.WriteTo(request, httpRequest.RouteValues[param.Name] as string);
        }

        foreach (var param in accessor.QueryStringParams)
        {
            param.WriteTo(request, httpRequest.Query[param.Name].FirstOrDefault());
        }

        foreach (var param in accessor.HeaderParams)
        {
            param.WriteTo(request, httpRequest.Headers[param.Name].FirstOrDefault());
        }

        await AddBodyParams(request, accessor, httpRequest.Body, cancellationToken).ConfigureAwait(false);

        return request;
    }

    private static async Task AddBodyParams<Request>(
        Request request,
        RequestAccessor<Request> accessor,
        Stream body,
        CancellationToken cancellationToken) 
        where Request : class, IRequest, new()
    {
        if (accessor.BodyParams.Any() && body != Stream.Null && body.CanRead)
        {
            try
            {
                var jsonBody = await JsonDocument.ParseAsync(body, cancellationToken: cancellationToken).ConfigureAwait(false);
                foreach (var param in accessor.BodyParams)
                {
                    param.WriteTo(request, jsonBody.RootElement.GetProperty(param.Name).GetString());
                }
            }
            catch (JsonException) { }
        }
    }
}