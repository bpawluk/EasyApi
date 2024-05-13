using BlazorUtils.EasyApi.Shared.Setup;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorUtils.EasyApi;

public static class SetupExtensions
{
    public static ContractBuilder AddEasyApi(this IServiceCollection services) => new(services);
}
