using BlazorUtils.EasyApi.Shared.Contract;
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
        RequestAccessor<Request> accessor,
        IHandle<Request> handler,
        CancellationToken cancellationToken)
        where Request : class, IRequest, new()
    {
        var request = await GetRequest(httpRequest, accessor).ConfigureAwait(false);
        await handler.Handle(request, cancellationToken).ConfigureAwait(false);
        return Results.Ok();
    }

    public async static Task<IResult> Handle<Request, Response>(
        HttpRequest httpRequest,
        RequestAccessor<Request> accessor,
        IHandle<Request, Response> handler, 
        CancellationToken cancellationToken)
        where Request : class, IRequest<Response>, new()
    {
        var request = await GetRequest(httpRequest, accessor).ConfigureAwait(false);
        var result = await handler.Handle(request, cancellationToken).ConfigureAwait(false);
        return Results.Ok(result);
    }

    private async static Task<Request> GetRequest<Request>(HttpRequest httpRequest, RequestAccessor<Request> accessor)
        where Request : class, IRequest, new()
    {
        var request = new Request();

        foreach (var param in accessor.GetRouteParams(request))
        {
            param.WriteTo(request, httpRequest.RouteValues[param.Name] as string);
        }

        foreach (var param in accessor.GetQueryStringParams(request))
        {
            param.WriteTo(request, httpRequest.Query[param.Name].FirstOrDefault());
        }

        foreach (var param in accessor.GetHeaderParams(request))
        {
            param.WriteTo(request, httpRequest.Headers[param.Name].FirstOrDefault());
        }

        await AddBodyParams(request, accessor, httpRequest.Body).ConfigureAwait(false);

        return request;
    }

    private static async Task AddBodyParams<Request>(Request request, RequestAccessor<Request> accessor, Stream body)
        where Request : class, IRequest, new()
    {
        if (body.CanRead)
        {
            var jsonBody = await JsonDocument.ParseAsync(body).ConfigureAwait(false);
            foreach (var param in accessor.GetBodyParams(request))
            {
                param.WriteTo(request, jsonBody.RootElement.GetProperty(param.Name).GetString());
            }
        }
    }
}