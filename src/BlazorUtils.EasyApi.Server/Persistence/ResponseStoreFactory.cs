using BlazorUtils.EasyApi.Server.Setup;
using BlazorUtils.EasyApi.Shared.Persistence;
using BlazorUtils.EasyApi.Shared.Rendering;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlazorUtils.EasyApi.Server.Persistence;

internal class ResponseStoreFactory(IServiceProvider services, IServerResponsePersistence responsePersistence) : IResponseStoreFactory
{
    private readonly IServiceProvider _services = services;
    private readonly IServerResponsePersistence _responsePersistence = responsePersistence;

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
