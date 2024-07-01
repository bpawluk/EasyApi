using BlazorUtils.EasyApi.Client.Persistence;
using BlazorUtils.EasyApi.Shared.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlazorUtils.EasyApi.Client.Setup;

public static class PersistenceSetup
{
    public static ClientBuilder Using(this ClientBuilder builder, PersistenceOptions persistenceOptions)
    {
        if (persistenceOptions.UsingPrerenderingState)
        {
            foreach (var request in builder.Requests.All)
            {
                if (request.ResponseType is Type responseType)
                {
                    typeof(PersistenceSetup).InvokeGeneric(
                        nameof(AddRequestWithResponsePersistence),
                        [request.RequestType, responseType],
                        builder.Services);
                }
            }
        }

        return builder;
    }

    private static void AddRequestWithResponsePersistence<Request, Response>(IServiceCollection services)
        where Request : class, IRequest<Response>, new()
    {
        services.AddScoped<IPersistentCall<Request, Response>, PersistentCaller<Request, Response>>();
    }
}
