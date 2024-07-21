using BlazorUtils.EasyApi.Server.Handling;
using BlazorUtils.EasyApi.Server.Rendering;
using BlazorUtils.EasyApi.Server.Setup;
using BlazorUtils.EasyApi.Shared.Exceptions;
using BlazorUtils.EasyApi.Shared.Persistence;
using BlazorUtils.EasyApi.Shared.Reflection;
using BlazorUtils.EasyApi.Shared.Rendering;
using BlazorUtils.EasyApi.Shared.Setup;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BlazorUtils.EasyApi.Server;

public static class ServerSetup
{
    public static ServerBuilder WithServer(this AppBuilder builder, params Assembly[] sources)
    {
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<IInteractivityDetector, InteractivityDetector>();
        builder.Services.AddScoped<IResponseStoreFactory, ResponseStoreFactory>();
        builder.Services.AddTransient<IEndpointsCustomization, NoEndpointsCustomization>();

        var defaultSource = Assembly.GetCallingAssembly();
        var handlers = sources
            .Append(defaultSource)
            .Distinct()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.Implements<IHandle>());

        foreach (var request in builder.Requests.All)
        {
            if (request.ResponseType is Type responseType)
            {
                typeof(ServerSetup).InvokeGeneric(
                    nameof(AddRequestWithResponse),
                    [request.RequestType, responseType],
                    builder.Services,
                    handlers);
            }
            else
            {
                typeof(ServerSetup).InvokeGeneric(
                    nameof(AddRequest),
                    request.RequestType,
                    builder.Services,
                    handlers);
            }
        }

        return new(builder);
    }

    private static void AddRequest<Request>(IServiceCollection services, IEnumerable<Type> handlers)
        where Request : class, IRequest, new()
    {
        var handlerInterface = typeof(IHandle<Request>);
        services.AddTransient(handlerInterface, FindHandler(handlers, handlerInterface));
        services.AddTransient<ICall<Request>, HandlerCaller<Request>>();
    }

    private static void AddRequestWithResponse<Request, Response>(IServiceCollection services, IEnumerable<Type> handlers)
        where Request : class, IRequest<Response>, new()
    {
        var handlerInterface = typeof(IHandle<Request, Response>);
        services.AddTransient(handlerInterface, FindHandler(handlers, handlerInterface));
        services.AddTransient<ICall<Request, Response>, HandlerCaller<Request, Response>>();
        services.AddTransient<IPersistentCall<Request, Response>, PersistentCaller<Request, Response>>();
    }

    private static Type FindHandler(IEnumerable<Type> handlers, Type handlerInterface)
    {
        var handler = handlers.SingleOrDefault(type => type.Implements(handlerInterface));
        return handler ?? throw new SetupException($"A request handler of type {handlerInterface.GetGenericName()} is not registered");
    }
}
