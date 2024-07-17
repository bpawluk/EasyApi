using BlazorUtils.EasyApi.Shared.Setup;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorUtils.EasyApi.Client.Setup;

public static class ResponsePersistenceSetup
{
    public static ClientBuilder Using<PrerenderedResponsePersistenceType>(
        this ClientBuilder builder, 
        ServiceLifetime lifetime = ServiceLifetime.Transient)
        where PrerenderedResponsePersistenceType : class, IPrerenderedResponsePersistence
    {
        return ((builder as AppBuilder).Using<PrerenderedResponsePersistence>(lifetime) as ClientBuilder)!;
    }

    public static ClientBuilder Using(this ClientBuilder builder, IPrerenderedResponsePersistence responsePersistence)
    {
        return ((builder as AppBuilder).Using(responsePersistence) as ClientBuilder)!;
    }
}
