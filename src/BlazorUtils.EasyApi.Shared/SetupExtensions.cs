using BlazorUtils.EasyApi.Shared.Setup;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorUtils.EasyApi;

public static class SetupExtensions
{
    public static ContractBuilder AddEasyApi(this IServiceCollection services) => new(services);

    // TODO: Configure customized Route/Query/Header serialization (CustomConvertersProvider)

    // TODO: Configure customized Body serialization

    // TODO: Configure endpoint extensions such as Authorization and others
}
