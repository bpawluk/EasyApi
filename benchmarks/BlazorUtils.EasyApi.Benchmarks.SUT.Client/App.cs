using BlazorUtils.EasyApi.Benchmarks.SUT.Contract;
using BlazorUtils.EasyApi.Client;
using BlazorUtils.EasyApi.Client.Setup;
using Microsoft.Extensions.Hosting;

namespace BlazorUtils.EasyApi.Benchmarks.SUT.Client;

public sealed class App(IHost app) : IDisposable
{
    private readonly IHost _app = app;

    public IServiceProvider Services => _app.Services;

    public static App Create(IHttpClientProvider httpClientProvider)
    {
        var builder = Host.CreateApplicationBuilder();
        builder.Services.AddEasyApi()
                        .WithContract(typeof(EmptyRequest).Assembly)
                        .WithClient()
                        .UsingHttpClientProvider(httpClientProvider);
        var app = builder.Build();
        app.Start();
        return new App(app);
    }

    public void Dispose()
    {
        _app.StopAsync().GetAwaiter().GetResult();
    }
}
