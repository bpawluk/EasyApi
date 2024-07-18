using BlazorUtils.EasyApi.Shared.Setup;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorUtils.EasyApi.Server.Setup;

public static class PrerenderedResponsePersistenceSetup
{
    public static ServerBuilder Using<PrerenderedResponsePersistenceType>(
        this ServerBuilder builder, 
        ServiceLifetime lifetime = ServiceLifetime.Transient)
        where PrerenderedResponsePersistenceType : class, IPrerenderedResponsePersistence
    {
        return (builder.UsingPrerendered<PrerenderedResponsePersistence>(lifetime) as ServerBuilder)!;
    }

    public static ServerBuilder Using(this ServerBuilder builder, IPrerenderedResponsePersistence responsePersistence)
    {
        return (builder.UsingPrerendered(responsePersistence) as ServerBuilder)!;
    }
}
