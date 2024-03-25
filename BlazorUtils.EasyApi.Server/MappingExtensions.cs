using BlazorUtils.EasyApi.Server.Http;
using BlazorUtils.EasyApi.Shared.Reflection;
using Microsoft.AspNetCore.Builder;
using System;
using System.Linq;
using System.Reflection;

namespace BlazorUtils.EasyApi.Server;

public static class MappingExtensions
{
    public static WebApplication MapRequests(this WebApplication app, Assembly contractSource)
    {
        var requests = contractSource.GetTypes().Where(t => typeof(IRequest).IsAssignableFrom(t));
        foreach (var request in requests)
        {
            if (request.GetInterfaces().FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>)) is Type requestIface)
            {
                var response = requestIface.GetGenericArguments().Single();
                typeof(MappingExtensions).InvokeGeneric(nameof(MapRequestWithResponse), new Type[] { request, response }, app);
            }
            else
            {
                typeof(MappingExtensions).InvokeGeneric(nameof(MapRequest), request, app);
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
