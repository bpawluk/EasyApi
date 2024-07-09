using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace BlazorUtils.EasyApi.Server.Setup;

public static class ResponsePersistenceSetup
{
    public static ServerBuilder Using<ResponsePersistenceType>(this ServerBuilder builder, ServiceLifetime lifetime = ServiceLifetime.Transient)
        where ResponsePersistenceType : class, IServerResponsePersistence
    {
        var descriptor = ServiceDescriptor.Describe(typeof(IServerResponsePersistence), typeof(ResponsePersistenceType), lifetime);
        builder.Services.Replace(descriptor);
        return builder;
    }

    public static ServerBuilder Using(this ServerBuilder builder, IServerResponsePersistence responsePersistence)
    {
        var descriptor = ServiceDescriptor.Singleton(responsePersistence);
        builder.Services.Replace(descriptor);
        return builder;
    }
}
