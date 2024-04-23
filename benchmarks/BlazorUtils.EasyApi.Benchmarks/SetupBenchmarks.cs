using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BlazorUtils.EasyApi.Benchmarks.SUT.Contract;
using BlazorUtils.EasyApi.Benchmarks.SUT.Server.Handlers;
using BlazorUtils.EasyApi.Client;
using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Shared.Setup;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace BlazorUtils.EasyApi.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
public class SetupBenchmarks
{
    [BenchmarkCategory("Client"), Benchmark(Baseline = true)]
    public HostApplicationBuilder ClientBaseline() => Host.CreateApplicationBuilder();

    [BenchmarkCategory("Client"), Benchmark]
    public AppBuilder ClientSetup() => Host.CreateApplicationBuilder()
        .Services
        .AddEasyApi()
        .WithContract(typeof(EmptyRequest).Assembly)
        .WithClient();

    [BenchmarkCategory("Server"), Benchmark(Baseline = true)]
    public WebApplication ServerBaseline() => WebApplication.CreateBuilder().Build();

    [BenchmarkCategory("Server"), Benchmark]
    public WebApplication ServerSetup()
    {
        var serverBuilder = WebApplication.CreateBuilder();
        serverBuilder.Services
            .AddEasyApi()
            .WithContract(typeof(EmptyRequest).Assembly)
            .WithServer(typeof(EmptyRequestHandler).Assembly);
        var serverApp = serverBuilder.Build();
        return serverApp.MapRequests();
    }
}