using BlazorUtils.EasyApi.Client.Setup;
using BlazorUtils.EasyApi.Shared.Persistence;
using BlazorUtils.EasyApi.Shared.Prerendering;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlazorUtils.EasyApi.Client.Persistence;

internal class ResponseStoreFactory(IServiceProvider services, IClientResponsePersistence responsePersistence) : IResponseStoreFactory
{
    private readonly IServiceProvider _services = services;
    private readonly IClientResponsePersistence _responsePersistence = responsePersistence;

    public IResponseStore<ResponseType>? GetStore<ResponseType>(IRequest<ResponseType> request)
    {
        var persistenceOptions = _responsePersistence.Configure(request);

        if (persistenceOptions.UsePrerenderedResponses)
        {
            return _services.GetRequiredService<PrerenderedResponseStore<ResponseType>>();
        }

        return null;
    }
}