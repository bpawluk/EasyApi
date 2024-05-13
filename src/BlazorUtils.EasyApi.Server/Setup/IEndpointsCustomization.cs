using Microsoft.AspNetCore.Builder;

namespace BlazorUtils.EasyApi.Server.Setup;

public interface IEndpointsCustomization
{
    void Customize<Request>(RouteHandlerBuilder builder);
}
