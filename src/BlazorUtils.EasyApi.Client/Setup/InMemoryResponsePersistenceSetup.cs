using BlazorUtils.EasyApi.Shared.Setup;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorUtils.EasyApi.Client.Setup;

public static class InMemoryResponsePersistenceSetup
{
    public static ClientBuilder Using<InMemoryResponsePersistenceType>(
        this ClientBuilder builder, 
        ServiceLifetime lifetime = ServiceLifetime.Transient)
        where InMemoryResponsePersistenceType : class, IInMemoryResponsePersistence
    {
        return (builder.UsingInMemory<InMemoryResponsePersistenceType>(lifetime) as ClientBuilder)!;
    }

    public static ClientBuilder Using(this ClientBuilder builder, IInMemoryResponsePersistence responsePersistence)
    {
        return (builder.UsingInMemory(responsePersistence) as ClientBuilder)!;
    }
}
