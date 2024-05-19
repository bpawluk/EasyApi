using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Server.Setup;
using BlazorUtils.EasyApi.Shared.Setup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace BlazorUtils.EasyApi.Tests.SetupTests.Server;

public sealed class EndpointsCustomizationTests : IAsyncDisposable
{
    private WebApplication _sut = default!;

    private async Task Initialize(Action<AppBuilder> additionalSetup)
    {
        var builder = WebApplication.CreateBuilder();

        builder.WebHost.UseTestServer();

        builder.Services
            .AddAuthorization()
            .AddAuthentication()
            .AddBearerToken();

        var easyApiBuilder = builder.Services
            .AddEasyApi()
            .WithContract(typeof(EndpointsCustomizationTestsDefaultRequest).Assembly)
            .WithServer();
        additionalSetup(easyApiBuilder);

        _sut = builder.Build();

        _sut.UseAuthorization();
        _sut.MapRequests();

        await _sut.StartAsync();
    }

    [Fact]
    public async Task CustomizedEndpoint_ByType_WorksAsSpecified()
    {
        await Initialize(appBuilder => appBuilder.UsingEndpointsCustomization<TestEndpointsCustomization>());
        var client = _sut.GetTestClient();
        var result = await client.GetAsync(nameof(EndpointsCustomizationTestsCustomizedRequest));
        Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
    }

    [Fact]
    public async Task CustomizedEndpoint_ByInstance_WorksAsSpecified()
    {
        await Initialize(appBuilder => appBuilder.UsingEndpointsCustomization(new TestEndpointsCustomization()));
        var client = _sut.GetTestClient();
        var result = await client.GetAsync(nameof(EndpointsCustomizationTestsCustomizedRequest));
        Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
    }

    [Fact]
    public async Task NotCustomizedEndpoint_DefaultBehavior()
    {
        await Initialize(appBuilder => appBuilder.UsingEndpointsCustomization<TestEndpointsCustomization>());
        var client = _sut.GetTestClient();
        var result = await client.GetAsync(nameof(EndpointsCustomizationTestsDefaultRequest));
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    public async ValueTask DisposeAsync()
    {
        await _sut.StopAsync();
        await _sut.DisposeAsync();
    }
}

internal class TestEndpointsCustomization : IEndpointsCustomization
{
    public void Customize<Request>(RouteHandlerBuilder builder)
    {
        if (typeof(Request) == typeof(EndpointsCustomizationTestsCustomizedRequest))
        {
            builder.RequireAuthorization();
        }
    }
}

internal class AuthorizationTestsRequestHandler
    : IHandle<EndpointsCustomizationTestsCustomizedRequest>
    , IHandle<EndpointsCustomizationTestsDefaultRequest>
{
    public Task<HttpResult> Handle(EndpointsCustomizationTestsCustomizedRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult.Ok());

    public Task<HttpResult> Handle(EndpointsCustomizationTestsDefaultRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult.Ok());
}

[Route(nameof(EndpointsCustomizationTestsCustomizedRequest))]
public class EndpointsCustomizationTestsCustomizedRequest : IGet { }

[Route(nameof(EndpointsCustomizationTestsDefaultRequest))]
public class EndpointsCustomizationTestsDefaultRequest : IGet { }
