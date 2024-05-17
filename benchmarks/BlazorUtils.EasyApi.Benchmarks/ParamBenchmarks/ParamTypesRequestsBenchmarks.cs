using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BlazorUtils.EasyApi.Benchmarks.SUT.Contract;
using BlazorUtils.EasyApi.Benchmarks.SUT.Contract.Data;
using BlazorUtils.EasyApi.Benchmarks.SUT.Contract.ParamTypes;
using System;
using System.Threading.Tasks;

namespace BlazorUtils.EasyApi.Benchmarks.ParamBenchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
public class ParamTypesRequestsBenchmarks : BenchmarksBase
{
    #region Requests
    private readonly EmptyRequest _emptyRequest = new();

    private readonly ClassParamsRequest _classParamsRequest = new()
    {
        BodyParam = new()
        {
            ProductId = 1,
            Name = "Watch",
            Price = 100M
        },
        HeaderParam = new()
        {
            CustomerId = 1,
            Name = "Adam Smith",
            Email = "asmith@example.com"
        },
        QueryStringParam = new()
        {
            Name = "Adam",
            Age = 30,
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Gender = Gender.Male,
            Address = new("Menlo Park", "94025", "1 Hacker Way")
        },
        RouteParam = null
    };

    private readonly CollectionParamsRequest _collectionParamsRequest = new()
    {
        BodyParam = [1, 2, 3],
        HeaderParam = ["one", "two", "three"],
        QueryStringParam = new([new("Menlo Park", "94025", "1 Hacker Way"), new("Mountain View", "94043", "1600 Amphitheatre Parkway")]),
        RouteParam = new([new(), null!, new() { CustomerId = 1, Name = "Adam", Email = "asmith@example.com" }])
    };

    private readonly PrimitiveParamsRequest _primitiveParamsRequest = new()
    {
        BodyParam = 1,
        HeaderParam = 10f,
        QueryStringParam = 100M,
        RouteParam = null
    };

    private readonly StructParamsRequest _structParamsRequest = new()
    {
        BodyParam = Guid.NewGuid(),
        HeaderParam = DateTime.Now,
        QueryStringParam = new("Menlo Park", "94025", "1 Hacker Way"),
        RouteParam = null
    };

    private readonly TextParamsRequest _textParamsRequest = new()
    {
        BodyParam = "text",
        HeaderParam = null,
        QueryStringParam = 'x',
        RouteParam = null
    };
    #endregion Requests

    [GlobalSetup]
    public void Setup() => SetupApp();

    [BenchmarkCategory("Client"), Benchmark(Baseline = true)]
    public async Task<HttpResult> ClientEmptyRequest() => await Call(_client!.Services, _emptyRequest);

    [BenchmarkCategory("Client"), Benchmark]
    public async Task<HttpResult> ClientClassParamsRequest() => await Call(_client!.Services, _classParamsRequest);

    [BenchmarkCategory("Client"), Benchmark]
    public async Task<HttpResult> ClientCollectionParamsRequest() => await Call(_client!.Services, _collectionParamsRequest);

    [BenchmarkCategory("Client"), Benchmark]
    public async Task<HttpResult> ClientPrimitiveParamsRequest() => await Call(_client!.Services, _primitiveParamsRequest);

    [BenchmarkCategory("Client"), Benchmark]
    public async Task<HttpResult> ClientStructParamsRequest() => await Call(_client!.Services, _structParamsRequest);

    [BenchmarkCategory("Client"), Benchmark]
    public async Task<HttpResult> ClientTextParamsRequest() => await Call(_client!.Services, _textParamsRequest);

    [BenchmarkCategory("Server"), Benchmark(Baseline = true)]
    public async Task<HttpResult> ServerEmptyRequest() => await Call(_server!.Services, _emptyRequest);

    [BenchmarkCategory("Server"), Benchmark]
    public async Task<HttpResult> ServerClassParamsRequest() => await Call(_server!.Services, _classParamsRequest);

    [BenchmarkCategory("Server"), Benchmark]
    public async Task<HttpResult> ServerCollectionParamsRequest() => await Call(_server!.Services, _collectionParamsRequest);

    [BenchmarkCategory("Server"), Benchmark]
    public async Task<HttpResult> ServerPrimitiveParamsRequest() => await Call(_server!.Services, _primitiveParamsRequest);

    [BenchmarkCategory("Server"), Benchmark]
    public async Task<HttpResult> ServerStructParamsRequest() => await Call(_server!.Services, _structParamsRequest);

    [BenchmarkCategory("Server"), Benchmark]
    public async Task<HttpResult> ServerTextParamsRequest() => await Call(_server!.Services, _textParamsRequest);

    [GlobalCleanup]
    public void Cleanup() => CleanupApp();
}
