using BlazorUtils.EasyApi.Server.Http;
using BlazorUtils.EasyApi.Shared.Contract;
using BlazorUtils.EasyApi.Shared.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BlazorUtils.EasyApi.Server;

public static class MappingExtensions
{
    public static WebApplication MapRequests(this WebApplication app)
    {
        var requests = app.Services.GetRequiredService<Requests>();
        foreach (var request in requests.All)
        {
            var requestType = request.RequestType;
            if (requestType.GetInterfaces().FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>)) is Type requestIface)
            {
                var responseType = requestIface.GetGenericArguments().Single();
                typeof(MappingExtensions).InvokeGeneric(nameof(MapRequestWithResponse), new Type[] { requestType, responseType }, app);
            }
            else
            {
                typeof(MappingExtensions).InvokeGeneric(nameof(MapRequest), requestType, app);
            }
        }
        return app;
    }

    public static void MapRequest<Request>(this WebApplication app)
        where Request : class, IRequest, new()
    {
        var route = RequestUtils.GetRoute<Request>();
        var method = new string[] { HttpMethodUtils.GetHttpMethod<Request>() };
        app.MapMethods(route, method, HttpHandler.Handle<Request>);
    }

    public static void MapRequestWithResponse<Request, Response>(this WebApplication app)
        where Request : class, IRequest<Response>, new()
    {
        var route = RequestUtils.GetRoute<Request>();
        var method = new string[] { HttpMethodUtils.GetHttpMethod<Request, Response>() };
        app.MapMethods(route, method, HttpHandler.Handle<Request, Response>);
    }
}
