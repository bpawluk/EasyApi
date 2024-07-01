using BlazorUtils.EasyApi.Shared.Setup;

namespace BlazorUtils.EasyApi.Client.Setup;

public class ClientBuilder : AppBuilder
{
    internal ClientBuilder(AppBuilder builder) : base(builder.Services, builder.Requests) { }
}
