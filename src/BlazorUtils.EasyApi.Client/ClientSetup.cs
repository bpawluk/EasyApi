using BlazorUtils.EasyApi.Client.Http;
using BlazorUtils.EasyApi.Client.Persistence;
using BlazorUtils.EasyApi.Client.Prerendering;
using BlazorUtils.EasyApi.Client.Setup;
using BlazorUtils.EasyApi.Shared.Persistence;
using BlazorUtils.EasyApi.Shared.Prerendering;
using BlazorUtils.EasyApi.Shared.Reflection;
using BlazorUtils.EasyApi.Shared.Setup;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlazorUtils.EasyApi.Client;

public static class ClientSetup
{
    public static ClientBuilder WithClient(this AppBuilder builder)
    {
        builder.Services.AddTransient<IHttpClientProvider, DefaultHttpClientProvider>();
        builder.Services.AddTransient<IClientResponsePersistence, NoResponsePersistence>();

        builder.Services.AddScoped<IPrerenderingDetector, PrerenderingDetector>();
        builder.Services.AddScoped<IResponseStoreFactory, ResponseStoreFactory>();
        builder.Services.AddScoped(typeof(PrerenderedResponseStore<>));

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
    }
}
