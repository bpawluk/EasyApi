using BlazorUtils.EasyApi.Client.Http;
using BlazorUtils.EasyApi.Shared.Contract;
using BlazorUtils.EasyApi.Shared.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;
using System.Reflection;

namespace BlazorUtils.EasyApi.Client;

public static class ClientExtensions
{
    public static IServiceCollection AddClient(this IServiceCollection services, Assembly contractSource)
    {
        var requests = contractSource.GetTypes().Where(t => typeof(IRequest).IsAssignableFrom(t));
        foreach (var request in requests)
        {
            if (request.GetInterfaces().FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>)) is Type requestIface)
            {
                var response = requestIface.GetGenericArguments().Single();
                typeof(ClientExtensions).InvokeGeneric(nameof(AddRequestWithResponse), new Type[] { request, response }, services);
            }
            else
            {
                typeof(ClientExtensions).InvokeGeneric(nameof(AddRequest), request, services);
            }
        }
        return services;
    }

    public static void AddRequest<Request>(IServiceCollection services)
        where Request : class, IRequest, new()
    {
        services.TryAddSingleton<RequestAccessor<Request>>();
        services.AddTransient<ICall<Request>, HttpCaller<Request>>();
    }

    public static void AddRequestWithResponse<Request, Response>(IServiceCollection services)
        where Request : class, IRequest<Response>, new()
    {
        services.TryAddSingleton<RequestAccessor<Request>>();
        services.AddTransient<ICall<Request, Response>, HttpCaller<Request, Response>>();
    }
}
