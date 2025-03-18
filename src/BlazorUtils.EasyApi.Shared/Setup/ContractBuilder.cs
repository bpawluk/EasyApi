using BlazorUtils.EasyApi.Shared.Contract;
using BlazorUtils.EasyApi.Shared.Json;
using BlazorUtils.EasyApi.Shared.Reflection;
using BlazorUtils.EasyApi.Shared.Serialization.Providers;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace BlazorUtils.EasyApi.Shared.Setup;

public class ContractBuilder
{
    internal IServiceCollection Services { get; }

    internal JsonOptionsProvider JsonOptions { get; }

    internal ContractBuilder(IServiceCollection services)
    {
        Services = services;
        JsonOptions = new();
    }

    public AppBuilder WithContract(params Assembly[] sources)
    {
        var convertersProvider = new ConvertersProvider(JsonOptions);
        var requests = sources
            .Distinct()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.Implements<IRequest>())
            .ToDictionary(type => type, type => (typeof(RequestAccessor<>).Apply(type).Create(convertersProvider) as RequestAccessor)!);
        var contract = new Requests(requests);
        Services.AddSingleton(contract);
        Services.AddSingleton(JsonOptions);
        return new(Services, contract);
    }
}
