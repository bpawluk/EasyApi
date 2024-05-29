using BlazorUtils.EasyApi.Shared.Setup;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BlazorUtils.EasyApi.Server.Setup;

public static class EndpointsCustomizationSetup
{
    public static AppBuilder Using<EndpointsCustomizationType>(this AppBuilder builder)
        where EndpointsCustomizationType : class, IEndpointsCustomization
    {
        var descriptor = ServiceDescriptor.Transient<IEndpointsCustomization, EndpointsCustomizationType>();
        builder.Services.Replace(descriptor);
        return builder;
    }

    public static AppBuilder Using(this AppBuilder builder, IEndpointsCustomization httpClientProvider)
    {
        var descriptor = ServiceDescriptor.Singleton(httpClientProvider);
        builder.Services.Replace(descriptor);
        return builder;
    }
}
