using BlazorUtils.EasyApi.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace BlazorUtils.EasyApi.UnitTests.HandlerTests;

public sealed class DependencyResolvingTests : IAsyncDisposable
{
    private readonly List<Guid> _calls = [];
    private WebApplication _sut = default!;

    private async Task Initialize(Action<IServiceCollection> additionalSetup)
    {
        var builder = WebApplication.CreateBuilder();

        builder.WebHost.UseTestServer();

        builder.Services.AddSingleton<DependencyUsedCallback>(OnDependencyUsed);
        additionalSetup(builder.Services);

        builder.Services
            .AddEasyApi()
            .WithContract(GetType().Assembly)
            .WithServer();

        _sut = builder.Build();
        _sut.MapRequests();

        await _sut.StartAsync();
    }

    [Fact]
    public async Task Handler_CalledByHttp_WithTransientDependency()
    {
        await Initialize(services => services.AddTransient<TestDependency>());
        await CallHttp(3);
        Assert.Equal(3, _calls.Distinct().Count());
    }

    [Fact]
    public async Task Handler_CalledByHttp_WithScopedDependency()
    {
        await Initialize(services => services.AddScoped<TestDependency>());
        await CallHttp(3);
        Assert.Equal(3, _calls.Distinct().Count()); // One scope per HTTP request
    }

    [Fact]
    public async Task Handler_CalledByHttp_WithSingletonDependency()
    {
        await Initialize(services => services.AddSingleton<TestDependency>());
        await CallHttp(3);
        Assert.Single(_calls.Distinct());
    }

    [Fact]
    public async Task Handler_CalledInternally_WithTransientDependency()
    {
        await Initialize(services => services.AddTransient<TestDependency>());
        await CallInternally(3);
        Assert.Equal(3, _calls.Distinct().Count());
    }

    [Fact]
    public async Task Handler_CalledInternally_WithScopedDependency()
    {
        await Initialize(services => services.AddScoped<TestDependency>());

        await CallInternally(2);

        var scope = _sut.Services.CreateScope();
        var caller = scope.ServiceProvider.GetRequiredService<ICall<DependencyResolvingTestsRequest>>();
        var result = await caller.CallHttp(new());
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(3, _calls.Count);

        scope = _sut.Services.CreateScope();
        caller = scope.ServiceProvider.GetRequiredService<ICall<DependencyResolvingTestsRequest>>();
        result = await caller.CallHttp(new());
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(4, _calls.Count);

        Assert.Equal(3, _calls.Distinct().Count());
    }

    [Fact]
    public async Task Handler_CalledInternally_WithSingletonDependency()
    {
        await Initialize(services => services.AddSingleton<TestDependency>());
        await CallInternally(3);
        Assert.Single(_calls.Distinct());
    }

    private async Task CallHttp(int times)
    {
        for (int currentCall = 1; currentCall <= times; currentCall++)
        {
            var client = _sut.GetTestClient();
            var result = await client.GetAsync(nameof(DependencyResolvingTestsRequest));
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(currentCall, _calls.Count);
        }
    }

    private async Task CallInternally(int times)
    {
        for (int currentCall = 1; currentCall <= times; currentCall++)
        {
            var caller = _sut.Services.GetRequiredService<ICall<DependencyResolvingTestsRequest>>();
            var result = await caller.CallHttp(new());
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(currentCall, _calls.Count);
        }
    }

    private void OnDependencyUsed(Guid providerId) => _calls.Add(providerId);

    public async ValueTask DisposeAsync()
    {
        await _sut.StopAsync();
        await _sut.DisposeAsync();
    }
}

[Route(nameof(DependencyResolvingTestsRequest))]
public class DependencyResolvingTestsRequest : IGet { }

internal class DependencyResolvingTestsRequestHandler(TestDependency TestDependency) : IHandle<DependencyResolvingTestsRequest>
{
    public Task<HttpResult> Handle(DependencyResolvingTestsRequest request, CancellationToken cancellationToken)
    {
        TestDependency.Acknowledge();
        return Task.FromResult(HttpResult.Ok());
    }
}

internal class TestDependency(DependencyUsedCallback DependencyUsed)
{
    private readonly Guid _id = Guid.NewGuid();

    public void Acknowledge() => DependencyUsed(_id);
}

internal delegate void DependencyUsedCallback(Guid providerId);
