using BlazorUtils.EasyApi.Shared.Setup;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorUtils.EasyApi.Client.Setup;

public static class PrerenderedResponsePersistenceSetup
{
    public static ClientBuilder Using<PrerenderedResponsePersistenceType>(
        this ClientBuilder builder, 
        ServiceLifetime lifetime = ServiceLifetime.Transient)
        where PrerenderedResponsePersistenceType : class, IPrerenderedResponsePersistence
    {
        return (builder.UsingPrerendered<PrerenderedResponsePersistence>(lifetime) as ClientBuilder)!;
    }

    public static ClientBuilder Using(this ClientBuilder builder, IPrerenderedResponsePersistence responsePersistence)
    {
        return (builder.UsingPrerendered(responsePersistence) as ClientBuilder)!;
    }
}
