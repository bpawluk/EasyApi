using BlazorUtils.EasyApi.Server.Http;
using BlazorUtils.EasyApi.Server.Setup;
using BlazorUtils.EasyApi.Shared.Contract;
using BlazorUtils.EasyApi.Shared.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlazorUtils.EasyApi.Server;

public static class MappingSetup
{
    public static WebApplication MapRequests(this WebApplication app)
    {
        var endpointsCustomization = app.Services.GetRequiredService<IEndpointsCustomization>();
        foreach (var request in app.Services.GetRequiredService<Requests>().All)
        {
            if (request.ResponseType is Type responseType)
            {
                typeof(MappingSetup).InvokeGeneric(
                    nameof(MapRequestWithResponse), 
                    new Type[] { request.RequestType, responseType }, 
                    request.Route,
                    app,
                    endpointsCustomization);
            }
            else
            {
                typeof(MappingSetup).InvokeGeneric(
                    nameof(MapRequest), 
                    request.RequestType, 
                    request.Route,
                    app,
                    endpointsCustomization);
            }
        }
        return app;
    }

    private static void MapRequest<Request>(string route, WebApplication app, IEndpointsCustomization customization)
        where Request : class, IRequest, new()
    {
        var method = new string[] { HttpMethodUtils.GetHttpMethod<Request>() };
        var builder = app.MapMethods(route, method, HttpHandler.Handle<Request>);
        customization.Customize<Request>(builder);
    }

    private static void MapRequestWithResponse<Request, Response>(string route, WebApplication app, IEndpointsCustomization customization)
        where Request : class, IRequest<Response>, new()
    {
        var method = new string[] { HttpMethodUtils.GetHttpMethod<Request, Response>() };
        var builder = app.MapMethods(route, method, HttpHandler.Handle<Request, Response>);
        customization.Customize<Request>(builder);
    }
}
