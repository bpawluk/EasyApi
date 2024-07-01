using BlazorUtils.EasyApi.Server.Persistence;
using BlazorUtils.EasyApi.Shared.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlazorUtils.EasyApi.Server.Setup;

public static class PersistenceSetup
{
    public static ServerBuilder Using(this ServerBuilder builder, PersistenceOptions persistenceOptions)
    {
        if (persistenceOptions.UsingPrerenderingState)
        {
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<PrerenderingDetector>();

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
        services.AddScoped<IPersistentCall<Request, Response>, PersistingCaller<Request, Response>>();
    }
}
