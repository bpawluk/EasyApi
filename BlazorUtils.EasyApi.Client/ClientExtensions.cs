﻿using BlazorUtils.EasyApi.Client.Http;
using BlazorUtils.EasyApi.Shared.Reflection;
using BlazorUtils.EasyApi.Shared.Setup;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlazorUtils.EasyApi.Client;

public static class ClientExtensions
{
    public static AppBuilder WithClient(this AppBuilder builder)
    {
        foreach (var request in builder.Requests.All)
        {
            if (request.ResponseType is Type responseType)
            {
                typeof(ClientExtensions).InvokeGeneric(
                    nameof(AddRequestWithResponse), 
                    new Type[] { request.RequestType, responseType }, 
                    builder.Services);
            }
            else
            {
                typeof(ClientExtensions).InvokeGeneric(
                    nameof(AddRequest),
                    request.RequestType, 
                    builder.Services);
            }
        }
        return builder;
    }

    public static void AddRequest<Request>(IServiceCollection services)
        where Request : class, IRequest, new()
    {
        services.AddTransient<ICall<Request>, HttpCaller<Request>>();
    }

    public static void AddRequestWithResponse<Request, Response>(IServiceCollection services)
        where Request : class, IRequest<Response>, new()
    {
        services.AddTransient<ICall<Request, Response>, HttpCaller<Request, Response>>();
    }
}
