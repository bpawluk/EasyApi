using BlazorUtils.EasyApi.Server.Handling;
using BlazorUtils.EasyApi.Shared.Contract;
using BlazorUtils.EasyApi.Shared.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BlazorUtils.EasyApi.Server;

public static class ServerExtensions
{
    public static IServiceCollection AddServer(this IServiceCollection services, Assembly contractSource)
    {
        var requests = contractSource.GetTypes().Where(t => typeof(IRequest).IsAssignableFrom(t));
        var handlers = Assembly.GetCallingAssembly().GetTypes().Where(t => typeof(IHandle).IsAssignableFrom(t));
        foreach (var request in requests)
        {
            if (request.GetInterfaces().FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>)) is Type requestIface)
            {
                var response = requestIface.GetGenericArguments().Single();
                typeof(ServerExtensions).InvokeGeneric(nameof(AddRequestWithResponse), new Type[] { request, response }, services, handlers);
            }
            else
            {
                typeof(ServerExtensions).InvokeGeneric(nameof(AddRequest), request, services, handlers);
            }
        }
        return services;
    }

    public static void AddRequest<Request>(IServiceCollection services, IEnumerable<Type> handlers)
        where Request : class, IRequest, new()
    {
        var handler = handlers.SingleOrDefault(t => typeof(IHandle<Request>).IsAssignableFrom(t) && !t.IsInterface)
            ?? throw new ArgumentException($"Could not locate a request handler for {typeof(Request).Name}");
        services.TryAddSingleton<RequestAccessor<Request>>();
        services.AddTransient<ICall<Request>, HandlerCaller<Request>>();
        services.AddTransient(typeof(IHandle<Request>), handler);
    }

    public static void AddRequestWithResponse<Request, Response>(IServiceCollection services, IEnumerable<Type> handlers)
        where Request : class, IRequest<Response>, new()
    {
        var handler = handlers.SingleOrDefault(t => typeof(IHandle<Request, Response>).IsAssignableFrom(t) && !t.IsInterface)
            ?? throw new ArgumentException($"Could not locate a request handler for {typeof(Request).Name}");
        services.TryAddSingleton<RequestAccessor<Request>>();
        services.AddTransient<ICall<Request, Response>, HandlerCaller<Request, Response>>();
        services.AddTransient(typeof(IHandle<Request, Response>), handler);
    }
}
