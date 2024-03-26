using BlazorUtils.EasyApi.Server.Handling;
using BlazorUtils.EasyApi.Shared.Reflection;
using BlazorUtils.EasyApi.Shared.Setup;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BlazorUtils.EasyApi.Server;

public static class ServerExtensions
{
    public static AppBuilder WithServer(this AppBuilder builder)
    {
        var handlers = Assembly.GetCallingAssembly().GetTypes().Where(t => typeof(IHandle).IsAssignableFrom(t));
        foreach (var request in builder.Requests.All)
        {
            var requestType = request.RequestType;
            if (request.ResponseType is Type responseType)
            {
                typeof(ServerExtensions).InvokeGeneric(nameof(AddRequestWithResponse), new Type[] { requestType, responseType }, builder.Services, handlers);
            }
            else
            {
                typeof(ServerExtensions).InvokeGeneric(nameof(AddRequest), requestType, builder.Services, handlers);
            }
        }
        return builder;
    }

    public static void AddRequest<Request>(IServiceCollection services, IEnumerable<Type> handlers)
        where Request : class, IRequest, new()
    {
        var handler = handlers.SingleOrDefault(t => typeof(IHandle<Request>).IsAssignableFrom(t) && !t.IsInterface)
            ?? throw new ArgumentException($"Could not locate a request handler for {typeof(Request).Name}");
        services.AddTransient<ICall<Request>, HandlerCaller<Request>>();
        services.AddTransient(typeof(IHandle<Request>), handler);
    }

    public static void AddRequestWithResponse<Request, Response>(IServiceCollection services, IEnumerable<Type> handlers)
        where Request : class, IRequest<Response>, new()
    {
        var handler = handlers.SingleOrDefault(t => typeof(IHandle<Request, Response>).IsAssignableFrom(t) && !t.IsInterface)
            ?? throw new ArgumentException($"Could not locate a request handler for {typeof(Request).Name}");
        services.AddTransient<ICall<Request, Response>, HandlerCaller<Request, Response>>();
        services.AddTransient(typeof(IHandle<Request, Response>), handler);
    }
}
