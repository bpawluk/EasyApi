using BlazorUtils.EasyApi.Server.Http;
using BlazorUtils.EasyApi.Shared.Contract;
using BlazorUtils.EasyApi.Shared.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlazorUtils.EasyApi.Server;

public static class MappingExtensions
{
    public static WebApplication MapRequests(this WebApplication app)
    {
        foreach (var request in app.Services.GetRequiredService<Requests>().All)
        {
            if (request.ResponseType is Type responseType)
            {
                typeof(MappingExtensions).InvokeGeneric(
                    nameof(MapRequestWithResponse), 
                    new Type[] { request.RequestType, responseType }, 
                    request.Route,
                    app);
            }
            else
            {
                typeof(MappingExtensions).InvokeGeneric(
                    nameof(MapRequest), 
                    request.RequestType, 
                    request.Route,
                    app);
            }
        }
        return app;
    }

    public static void MapRequest<Request>(string route, WebApplication app)
        where Request : class, IRequest, new()
    {
        var method = new string[] { HttpMethodUtils.GetHttpMethod<Request>() };
        app.MapMethods(route, method, HttpHandler.Handle<Request>);
    }

    public static void MapRequestWithResponse<Request, Response>(string route, WebApplication app)
        where Request : class, IRequest<Response>, new()
    {
        var method = new string[] { HttpMethodUtils.GetHttpMethod<Request, Response>() };
        app.MapMethods(route, method, HttpHandler.Handle<Request, Response>);
    }
}
