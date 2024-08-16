using BlazorUtils.EasyApi.Shared.Persistence.InMemory;
using BlazorUtils.EasyApi.Shared.Persistence.Prerendered;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlazorUtils.EasyApi.Shared.Setup;

internal static class ResponsePersistenceSetup
{
    public static AppBuilder UsingPrerendered<PrerenderedResponsePersistenceType>(
        this AppBuilder builder, 
        ServiceLifetime lifetime)
        where PrerenderedResponsePersistenceType : class, IPrerenderedResponsePersistence
    {
        builder.Services.AddScoped(typeof(PrerenderedResponseStore<>));
        builder.Services.AddResponsePersistence(typeof(PrerenderedResponsePersistenceType), lifetime);
        return builder;
    }

    public static AppBuilder UsingPrerendered(
        this AppBuilder builder, 
        IPrerenderedResponsePersistence responsePersistence)
    {
        builder.Services.AddScoped(typeof(PrerenderedResponseStore<>));
        builder.Services.AddResponsePersistence(responsePersistence);
        return builder;
    }

    public static AppBuilder UsingInMemory<InMemoryResponsePersistenceType>(
        this AppBuilder builder,
        ServiceLifetime lifetime)
        where InMemoryResponsePersistenceType : class, IInMemoryResponsePersistence
    {
        builder.Services.AddScoped<MemoryCacheProvider>();
        builder.Services.AddScoped(typeof(InMemoryResponseStore<>));
        builder.Services.AddResponsePersistence(typeof(InMemoryResponsePersistenceType), lifetime);
        return builder;
    }

    public static AppBuilder UsingInMemory(
        this AppBuilder builder,
        IInMemoryResponsePersistence responsePersistence)
    {
        builder.Services.AddScoped<MemoryCacheProvider>();
        builder.Services.AddScoped(typeof(InMemoryResponseStore<>));
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
