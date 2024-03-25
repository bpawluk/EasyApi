using BlazorUtils.EasyApi.Client.Http;
using BlazorUtils.EasyApi.Shared.Contract;
using BlazorUtils.EasyApi.Shared.Reflection;
using BlazorUtils.EasyApi.Shared.Setup;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;

namespace BlazorUtils.EasyApi.Client;

public static class ClientExtensions
{
    public static AppBuilder WithClient(this AppBuilder builder)
    {
        foreach (var request in builder.Requests.All)
        {
            var requestType = request.RequestType;
            if (requestType.GetInterfaces().FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>)) is Type requestInterface)
            {
                var responseType = requestInterface.GetGenericArguments().Single();
                typeof(ClientExtensions).InvokeGeneric(nameof(AddRequestWithResponse), new Type[] { requestType, responseType }, builder.Services);
            }
            else
            {
                typeof(ClientExtensions).InvokeGeneric(nameof(AddRequest), requestType, builder.Services);
            }
        }
        return builder;
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
