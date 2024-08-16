using BlazorUtils.EasyApi.Shared.Persistence.Compound;
using BlazorUtils.EasyApi.Shared.Persistence.InMemory;
using BlazorUtils.EasyApi.Shared.Persistence.Prerendered;
using BlazorUtils.EasyApi.Shared.Rendering;
using BlazorUtils.EasyApi.Shared.Setup;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BlazorUtils.EasyApi.Shared.Persistence;

internal interface IResponseStoreFactory
{
    IResponseStore<ResponseType>? GetStore<ResponseType>(IRequest<ResponseType> request);
}

internal class ResponseStoreFactory(IServiceProvider services) : IResponseStoreFactory
{
    private readonly IServiceProvider _services = services;

    public IResponseStore<ResponseType>? GetStore<ResponseType>(IRequest<ResponseType> request)
    {
        var responsePersistenceConfigurations = _services.GetServices<IResponsePersistence>();

        var stores = responsePersistenceConfigurations
            .Select(config => GetStore(config, request))
            .Where(store => store is not null)
            .ToArray();

        if (stores.Length == 0)
        {
            return null;
        }
        else if (stores.Length == 1)
        {
            return stores.Single();
        }
        else
        {
            return new CompoundResponseStore<ResponseType>(stores!);
        }
    }

    private IResponseStore<ResponseType>? GetStore<ResponseType>(
        IResponsePersistence configuration,
        IRequest<ResponseType> request)
    {
        return configuration switch
        {
            IPrerenderedResponsePersistence prpConfig => GetPrerenderedResponseStore(prpConfig, request),
            IInMemoryResponsePersistence impConfig => GetInMemoryResponseStore(impConfig, request),
            _ => null
        };
    }

    private PrerenderedResponseStore<ResponseType>? GetPrerenderedResponseStore<ResponseType>(
        IPrerenderedResponsePersistence configuration,
        IRequest<ResponseType> request)
    {
        return configuration.Configure(request).IsEnabled
            ? _services.GetRequiredService<PrerenderedResponseStore<ResponseType>>()
            : null;
    }

    private InMemoryResponseStore<ResponseType>? GetInMemoryResponseStore<ResponseType>(
        IInMemoryResponsePersistence configuration,
        IRequest<ResponseType> request)
    {
        var storeOptions = configuration.Configure(request);
        return storeOptions.IsEnabled
            ? new(
                _services.GetRequiredService<MemoryCacheProvider>().Cache,
                _services.GetRequiredService<IInteractivityDetector>(),
                storeOptions.AbsoluteExpiration,
                storeOptions.SlidingExpiration)
            : null;
    }
}
