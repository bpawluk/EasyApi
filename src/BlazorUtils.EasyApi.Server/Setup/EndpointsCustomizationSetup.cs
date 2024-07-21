using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BlazorUtils.EasyApi.Server.Setup;

public static class EndpointsCustomizationSetup
{
    public static ServerBuilder Using<EndpointsCustomizationType>(this ServerBuilder builder)
        where EndpointsCustomizationType : class, IEndpointsCustomization
    {
        var descriptor = ServiceDescriptor.Transient<IEndpointsCustomization, EndpointsCustomizationType>();
        builder.Services.Replace(descriptor);
        return builder;
    }

    public static ServerBuilder Using(this ServerBuilder builder, IEndpointsCustomization httpClientProvider)
    {
        var descriptor = ServiceDescriptor.Singleton(httpClientProvider);
        builder.Services.Replace(descriptor);
        return builder;
    }
}
