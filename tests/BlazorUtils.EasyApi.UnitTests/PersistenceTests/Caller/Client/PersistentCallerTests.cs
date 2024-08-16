using BlazorUtils.EasyApi.Client;
using BlazorUtils.EasyApi.Client.Setup;
using BlazorUtils.EasyApi.UnitTests.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorUtils.EasyApi.UnitTests.PersistenceTests.Caller.Client;

public class PersistentCallerTests : PersistentCallerTestsBase
{
    protected override Task<IServiceProvider> CreateSUT(Action<IServiceCollection> servicesOverride)
    {
        var services = new ServiceCollection();

        services
            .AddEasyApi()
            .WithContract(GetType().Assembly)
            .WithClient()
            .Using<TestHttpClientProvider>();

        servicesOverride(services);

        return Task.FromResult<IServiceProvider>(services.BuildServiceProvider());
    }

    public override Task DisposeAsync() => Task.CompletedTask;
}
