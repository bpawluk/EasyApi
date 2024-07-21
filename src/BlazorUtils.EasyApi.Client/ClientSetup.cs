using BlazorUtils.EasyApi.Client.Http;
using BlazorUtils.EasyApi.Client.Rendering;
using BlazorUtils.EasyApi.Client.Setup;
using BlazorUtils.EasyApi.Shared.Persistence;
using BlazorUtils.EasyApi.Shared.Reflection;
using BlazorUtils.EasyApi.Shared.Rendering;
using BlazorUtils.EasyApi.Shared.Setup;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlazorUtils.EasyApi.Client;

public static class ClientSetup
{
    public static ClientBuilder WithClient(this AppBuilder builder)
    {
        builder.Services.AddScoped<IInteractivityDetector, InteractivityDetector>();
        builder.Services.AddScoped<IResponseStoreFactory, ResponseStoreFactory>();
        builder.Services.AddTransient<IHttpClientProvider, DefaultHttpClientProvider>();

        foreach (var request in builder.Requests.All)
        {
            if (request.ResponseType is Type responseType)
            {
                typeof(ClientSetup).InvokeGeneric(
                    nameof(AddRequestWithResponse), 
                    [request.RequestType, responseType], 
                    builder.Services);
            }
            else
            {
                typeof(ClientSetup).InvokeGeneric(
                    nameof(AddRequest),
                    request.RequestType, 
                    builder.Services);
            }
        }
        return new(builder);
    }

    private static void AddRequest<Request>(IServiceCollection services)
        where Request : class, IRequest, new()
    {
        services.AddTransient<ICall<Request>, HttpCaller<Request>>();
    }

    private static void AddRequestWithResponse<Request, Response>(IServiceCollection services)
        where Request : class, IRequest<Response>, new()
    {
        services.AddTransient<ICall<Request, Response>, HttpCaller<Request, Response>>();
        services.AddTransient<IPersistentCall<Request, Response>, PersistentCaller<Request, Response>>();
    }
}
