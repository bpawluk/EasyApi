using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Server.Setup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorUtils.EasyApi.UnitTests.PersistenceTests.Caller.Server;

public class PersistentCallerTests : PersistentCallerTestsBase
{
    private WebApplication _webApp = default!;

    protected override async Task<IServiceProvider> CreateSUT(Action<IServiceCollection> servicesOverride)
    {
        var builder = WebApplication.CreateBuilder();

        builder.WebHost.UseTestServer();

        builder.Services
            .AddEasyApi()
            .WithContract(GetType().Assembly)
            .WithServer();

        servicesOverride(builder.Services);

        _webApp = builder.Build();

        _webApp.MapRequests();

        await _webApp.StartAsync();

        return _webApp.Services;
    }

    public async override Task DisposeAsync()
    {
        await _webApp.StopAsync();
        await _webApp.DisposeAsync();
    }
}
