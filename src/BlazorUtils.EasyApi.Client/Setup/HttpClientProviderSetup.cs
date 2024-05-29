using BlazorUtils.EasyApi.Shared.Setup;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BlazorUtils.EasyApi.Client.Setup;

public static class HttpClientProviderSetup
{
    public static AppBuilder Using<HttpClientProviderType>(this AppBuilder builder, ServiceLifetime lifetime = ServiceLifetime.Transient)
        where HttpClientProviderType : class, IHttpClientProvider
    {
        var descriptor = ServiceDescriptor.Describe(typeof(IHttpClientProvider), typeof(HttpClientProviderType), lifetime);
        builder.Services.Replace(descriptor);
        return builder;
    }

    public static AppBuilder Using(this AppBuilder builder, IHttpClientProvider httpClientProvider)
    {
        var descriptor = ServiceDescriptor.Singleton(httpClientProvider);
        builder.Services.Replace(descriptor);
        return builder;
    }
}
