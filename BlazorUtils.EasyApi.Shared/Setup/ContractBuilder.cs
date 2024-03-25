using BlazorUtils.EasyApi.Shared.Contract;
using BlazorUtils.EasyApi.Shared.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace BlazorUtils.EasyApi.Shared.Setup;

public class ContractBuilder
{
    private readonly IServiceCollection _services;

    internal ContractBuilder(IServiceCollection services)
    {
        _services = services;
    }

    public AppBuilder WithContract(params Assembly[] sources)
    {
        var requests = sources
            .Distinct()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(t => typeof(IRequest).IsAssignableFrom(t))
            .ToDictionary(t => t, t => (typeof(RequestAccessor<>).Apply(t).Create() as RequestAccessor)!);
        var contract = new Requests(requests);
        _services.AddSingleton(contract);
        return new(_services, contract);
    }
}
