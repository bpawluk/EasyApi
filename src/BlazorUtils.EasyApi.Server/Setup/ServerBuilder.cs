using BlazorUtils.EasyApi.Shared.Setup;

namespace BlazorUtils.EasyApi.Server.Setup;

public class ServerBuilder : AppBuilder
{
    internal ServerBuilder(AppBuilder builder) : base(builder.Services, builder.Requests) { }
}
