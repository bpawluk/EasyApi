using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BlazorUtils.EasyApi.Client.Setup;

public static class HttpClientProviderSetup
{
    public static ClientBuilder Using<HttpClientProviderType>(this ClientBuilder builder, ServiceLifetime lifetime = ServiceLifetime.Transient)
        where HttpClientProviderType : class, IHttpClientProvider
    {
        var descriptor = ServiceDescriptor.Describe(typeof(IHttpClientProvider), typeof(HttpClientProviderType), lifetime);
        builder.Services.Replace(descriptor);
        return builder;
    }

    public static ClientBuilder Using(this ClientBuilder builder, IHttpClientProvider httpClientProvider)
    {
        var descriptor = ServiceDescriptor.Singleton(httpClientProvider);
        builder.Services.Replace(descriptor);
        return builder;
    }
}
