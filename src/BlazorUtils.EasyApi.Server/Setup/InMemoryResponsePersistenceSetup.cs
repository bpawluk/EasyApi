using BlazorUtils.EasyApi.Shared.Setup;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorUtils.EasyApi.Server.Setup;

public static class InMemoryResponsePersistenceSetup
{
    public static ServerBuilder Using<InMemoryResponsePersistenceType>(
        this ServerBuilder builder, 
        ServiceLifetime lifetime = ServiceLifetime.Transient)
        where InMemoryResponsePersistenceType : class, IInMemoryResponsePersistence
    {
        return (builder.UsingInMemory<InMemoryResponsePersistenceType>(lifetime) as ServerBuilder)!;
    }

    public static ServerBuilder Using(this ServerBuilder builder, IInMemoryResponsePersistence responsePersistence)
    {
        return (builder.UsingInMemory(responsePersistence) as ServerBuilder)!;
    }
}
