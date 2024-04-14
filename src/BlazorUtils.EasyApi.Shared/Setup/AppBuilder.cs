using BlazorUtils.EasyApi.Shared.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorUtils.EasyApi.Shared.Setup;

public class AppBuilder
{
    internal Requests Requests { get; }

    internal IServiceCollection Services { get; }

    internal AppBuilder(IServiceCollection services, Requests requests)
    {
        Services = services;
        Requests = requests;
    }
}
