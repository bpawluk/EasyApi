using BlazorUtils.EasyApi.Benchmarks.SUT.Client;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorUtils.EasyApi.Benchmarks;

public class BenchmarksBase
{
    protected App? _client { get; set; }
    protected WebApplicationFactory<SUT.Server.Program>? _server { get; set; }

    protected void SetupApp()
    {
        var factory = new WebApplicationFactory<SUT.Server.Program>();
        var httpClient = factory.CreateClient();
        var httpClientProvider = new HttpClientProvider(httpClient);
        _client = App.Create(httpClientProvider);
        _server = factory;
    }

    protected static async Task<HttpResult> Call<Request>(IServiceProvider services, Request request) where Request : class, IRequest, new()
    {
        var caller = services.GetRequiredService<ICall<Request>>();
        return await caller.CallHttp(request, CancellationToken.None);
    }

    protected void CleanupApp()
    {
        _client!.Dispose();
        _server!.Dispose();
    }
}
