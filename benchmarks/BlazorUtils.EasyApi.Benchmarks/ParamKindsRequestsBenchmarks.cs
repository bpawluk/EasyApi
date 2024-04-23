using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BlazorUtils.EasyApi.Benchmarks.SUT.Contract;
using BlazorUtils.EasyApi.Benchmarks.SUT.Contract.Data;
using BlazorUtils.EasyApi.Benchmarks.SUT.Contract.ParamKinds;
using System;
using System.Threading.Tasks;

namespace BlazorUtils.EasyApi.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
public class ParamKindsRequestsBenchmarks : BenchmarksBase
{
    #region Requests
    private readonly EmptyRequest _emptyRequest = new();

    private readonly BodyParamsRequest _bodyParamsRequest = new()
    {
        Integer = int.MaxValue,
        NullableFloat = float.MaxValue,
        Character = 'x',
        String = "text",
        Enum = Gender.Other,
        Class = new()
        {
            Name = "Adam",
            Age = 30,
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Gender = Gender.Male,
            Address = new("Menlo Park", "94025", "1 Hacker Way")
        }
    };

    private readonly HeaderParamsRequest _headerParamsRequest = new()
    {
        Integer = int.MaxValue,
        NullableFloat = float.MaxValue,
        Character = 'x',
        String = "text",
        Enum = Gender.Other,
        Class = new()
        {
            Name = "Adam",
            Age = 30,
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Gender = Gender.Male,
            Address = new("Menlo Park", "94025", "1 Hacker Way")
        }
    };

    private readonly QueryStringParamsRequest _queryStringParamsRequest = new()
    {
        Integer = int.MaxValue,
        NullableFloat = float.MaxValue,
        Character = 'x',
        String = "text",
        Enum = Gender.Other,
        Class = new()
        {
            Name = "Adam",
            Age = 30,
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Gender = Gender.Male,
            Address = new("Menlo Park", "94025", "1 Hacker Way")
        }
    };

    private readonly RouteParamsRequest _routeParamsRequest = new()
    {
        Integer = int.MaxValue,
        NullableFloat = float.MaxValue,
        Character = 'x',
        String = "text",
        Enum = Gender.Other,
        Class = new()
        {
            Name = "Adam",
            Age = 30,
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Gender = Gender.Male,
            Address = new("Menlo Park", "94025", "1 Hacker Way")
        }
    };
    #endregion Requests

    [GlobalSetup]
    public void Setup() => SetupApp();

    [BenchmarkCategory("Client"), Benchmark(Baseline = true)]
    public async Task<HttpResult> ClientEmptyRequest() => await Call(_client!.Services, _emptyRequest);

    [BenchmarkCategory("Client"), Benchmark]
    public async Task<HttpResult> ClientBodyParamsRequest() => await Call(_client!.Services, _bodyParamsRequest);

    [BenchmarkCategory("Client"), Benchmark]
    public async Task<HttpResult> ClientHeaderParamsRequest() => await Call(_client!.Services, _headerParamsRequest);

    [BenchmarkCategory("Client"), Benchmark]
    public async Task<HttpResult> ClientQueryStringParamsRequest() => await Call(_client!.Services, _queryStringParamsRequest);

    [BenchmarkCategory("Client"), Benchmark]
    public async Task<HttpResult> ClientRouteParamsParamsRequest() => await Call(_client!.Services, _routeParamsRequest);

    [BenchmarkCategory("Server"), Benchmark(Baseline = true)]
    public async Task<HttpResult> ServerEmptyRequest() => await Call(_server!.Services, _emptyRequest);

    [BenchmarkCategory("Server"), Benchmark]
    public async Task<HttpResult> ServerBodyParamsRequest() => await Call(_server!.Services, _bodyParamsRequest);

    [BenchmarkCategory("Server"), Benchmark]
    public async Task<HttpResult> ServerHeaderParamsRequest() => await Call(_server!.Services, _headerParamsRequest);

    [BenchmarkCategory("Server"), Benchmark]
    public async Task<HttpResult> ServerQueryStringParamsRequest() => await Call(_server!.Services, _queryStringParamsRequest);

    [BenchmarkCategory("Server"), Benchmark]
    public async Task<HttpResult> ServerRouteParamsRequest() => await Call(_server!.Services, _routeParamsRequest);

    [GlobalCleanup]
    public void Cleanup() => CleanupApp();
}
