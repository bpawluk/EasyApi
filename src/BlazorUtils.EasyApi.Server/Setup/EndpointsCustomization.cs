using Microsoft.AspNetCore.Builder;

namespace BlazorUtils.EasyApi.Server.Setup;

internal class EndpointsCustomization : IEndpointsCustomization
{
    public void Customize<Request>(RouteHandlerBuilder builder) { }
}
