using BlazorUtils.EasyApi.Server.Http;
using BlazorUtils.EasyApi.Server.Setup;
using BlazorUtils.EasyApi.Shared.Contract;
using BlazorUtils.EasyApi.Shared.Reflection;
using Microsoft.AspNetCore.Authorization;
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
                    request.RouteInfo,
                    app,
                    endpointsCustomization);
            }
            else
            {
                typeof(MappingSetup).InvokeGeneric(
                    nameof(MapRequest),
                    request.RequestType,
                    request.RouteInfo,
                    app,
                    endpointsCustomization);
            }
        }
        return app;
    }

    private static void MapRequest<Request>(RouteInfo routeInfo, WebApplication app, IEndpointsCustomization customization)
        where Request : class, IRequest, new()
    {
        var method = new string[] { HttpMethodUtils.GetHttpMethod<Request>() };
        var builder = app.MapMethods(routeInfo.Value, method, HttpHandler.Handle<Request>);
        SetupEndpoint<Request>(builder, routeInfo, customization);
    }

    private static void MapRequestWithResponse<Request, Response>(RouteInfo routeInfo, WebApplication app, IEndpointsCustomization customization)
        where Request : class, IRequest<Response>, new()
    {
        var method = new string[] { HttpMethodUtils.GetHttpMethod<Request, Response>() };
        var builder = app.MapMethods(routeInfo.Value, method, HttpHandler.Handle<Request, Response>);
        SetupEndpoint<Request>(builder, routeInfo, customization);
    }

    private static void SetupEndpoint<Request>(RouteHandlerBuilder builder, RouteInfo routeInfo, IEndpointsCustomization customization)
        where Request : class, IRequest, new()
    {
        if (routeInfo.Authorization.Authorize)
        {
            var authorizeData = new AuthorizeAttribute()
            {
                Policy = routeInfo.Authorization.Policy,
                Roles = routeInfo.Authorization.Roles,
                AuthenticationSchemes = routeInfo.Authorization.AuthenticationSchemes
            };
            builder.RequireAuthorization(authorizeData);
        }
        customization.Customize<Request>(builder);
    }
}
