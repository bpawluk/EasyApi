using BlazorUtils.EasyApi.Shared.Rendering;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlazorUtils.EasyApi.Shared.Setup;

internal static class ResponsePersistenceSetup
{
    public static AppBuilder Using<PrerenderedResponsePersistenceType>(
        this AppBuilder builder, 
        ServiceLifetime lifetime)
        where PrerenderedResponsePersistenceType : class, IPrerenderedResponsePersistence
    {
        builder.Services.AddScoped(typeof(PrerenderedResponseStore<>));
        builder.Services.AddResponsePersistence(typeof(PrerenderedResponsePersistenceType), lifetime);
        return builder;
    }

    public static AppBuilder Using(
        this AppBuilder builder, 
        IPrerenderedResponsePersistence responsePersistence)
    {
        builder.Services.AddScoped(typeof(PrerenderedResponseStore<>));
        builder.Services.AddResponsePersistence(responsePersistence);
        return builder;
    }

    private static void AddResponsePersistence(
        this IServiceCollection services, 
        Type persistenceType, 
        ServiceLifetime lifetime)
    {
        var descriptor = ServiceDescriptor.Describe(typeof(IResponsePersistence), persistenceType, lifetime);
        services.Add(descriptor);
    }

    private static void AddResponsePersistence(
        this IServiceCollection services, 
        IResponsePersistence persistenceInstance)
    {
        services.AddSingleton(persistenceInstance);
    }
}
