using BlazorUtils.EasyApi.Shared.Setup;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BlazorUtils.EasyApi.Client.Setup;

public static class AdditionalClientExtensions
{
    public static AppBuilder UsingHttpClientProvider<HttpClientProviderType>(this AppBuilder builder)
        where HttpClientProviderType : class, IHttpClientProvider
    {
        builder.Services.Replace(ServiceDescriptor.Transient<IHttpClientProvider, HttpClientProviderType>());
        return builder;
    }

    public static AppBuilder UsingHttpClientProvider(this AppBuilder builder, IHttpClientProvider httpClientProvider)
    {
        builder.Services.Replace(ServiceDescriptor.Singleton(httpClientProvider));
        return builder;
    }
}
