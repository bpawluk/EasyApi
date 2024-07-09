using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace BlazorUtils.EasyApi.Client.Setup;

public static class ResponsePersistenceSetup
{
    public static ClientBuilder Using<ResponsePersistenceType>(this ClientBuilder builder, ServiceLifetime lifetime = ServiceLifetime.Transient)
        where ResponsePersistenceType : class, IClientResponsePersistence
    {
        var descriptor = ServiceDescriptor.Describe(typeof(IClientResponsePersistence), typeof(ResponsePersistenceType), lifetime);
        builder.Services.Replace(descriptor);
        return builder;
    }

    public static ClientBuilder Using(this ClientBuilder builder, IClientResponsePersistence responsePersistence)
    {
        var descriptor = ServiceDescriptor.Singleton(responsePersistence);
        builder.Services.Replace(descriptor);
        return builder;
    }
}
