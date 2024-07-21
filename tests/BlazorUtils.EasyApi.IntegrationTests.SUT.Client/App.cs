using BlazorUtils.EasyApi.Client;
using BlazorUtils.EasyApi.Client.Setup;
using BlazorUtils.EasyApi.IntegrationTests.SUT.Contract;
using BlazorUtils.EasyApi.IntegrationTests.SUT.Contract.Params;
using BlazorUtils.EasyApi.IntegrationTests.SUT.Contract.Response;
using Microsoft.Extensions.Hosting;

namespace BlazorUtils.EasyApi.IntegrationTests.SUT.Client;

public sealed class App(IHost app) : IDisposable
{
    private readonly IHost _app = app;

    public IServiceProvider Services => _app.Services;

    public static App Create(IHttpClientProvider httpClientProvider)
    {
        var builder = Host.CreateApplicationBuilder();
        builder.Services.AddEasyApi()
                        .WithContract(
                            typeof(GetRequest).Assembly,
                            typeof(HeaderParamRequest).Assembly,
                            typeof(NoResponseRequest).Assembly)
                        .WithClient()
                        .Using(httpClientProvider);
        var app = builder.Build();
        app.Start();
        return new App(app);
    }

    public void Dispose()
    {
        _app.StopAsync().GetAwaiter().GetResult();
    }
}
