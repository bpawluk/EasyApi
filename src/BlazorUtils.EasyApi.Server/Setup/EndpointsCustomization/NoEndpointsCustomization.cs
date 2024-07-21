using Microsoft.AspNetCore.Builder;

namespace BlazorUtils.EasyApi.Server.Setup;

internal class NoEndpointsCustomization : IEndpointsCustomization
{
    public void Customize<Request>(RouteHandlerBuilder builder) { }
}
