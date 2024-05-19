using BlazorUtils.EasyApi.Server;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace BlazorUtils.EasyApi.Tests.SecurityTests;

public sealed class AuthorizationTests : IAsyncDisposable
{
    private WebApplication _sut = default!;

    private async Task Initialize(bool shouldAuthenticate)
    {
        var builder = WebApplication.CreateBuilder();

        builder.WebHost.UseTestServer();

        builder.Services
            .AddAuthorization()
            .AddAuthentication(defaultScheme: TestAuthHandler.AuthScheme)
            .AddScheme<TestAuthOptions, TestAuthHandler>(TestAuthHandler.AuthScheme, options =>
            {
                options.ShouldAuthenticate = shouldAuthenticate;
            });

        builder.Services
            .AddEasyApi()
            .WithContract(GetType().Assembly)
            .WithServer();

        _sut = builder.Build();

        _sut.UseAuthentication();
        _sut.UseAuthorization();
        _sut.MapRequests();

        await _sut.StartAsync();
    }

    [Fact]
    public async Task PublicRoute_NoUser()
    {
        await Initialize(false);
        var client = _sut.GetTestClient();
        var result = await client.GetAsync(nameof(AuthorizationTestsPublicRequest));
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task PublicRoute_AuthenticatedUser()
    {
        await Initialize(true);
        var client = _sut.GetTestClient();
        var result = await client.GetAsync(nameof(AuthorizationTestsPublicRequest));
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task ProtectedRoute_NoUser()
    {
        await Initialize(false);
        var client = _sut.GetTestClient();
        var result = await client.GetAsync(nameof(AuthorizationTestsProtectedRequest));
        Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
    }

    [Fact]
    public async Task ProtectedRoute_AuthenticatedUser()
    {
        await Initialize(true);
        var client = _sut.GetTestClient();
        var result = await client.GetAsync(nameof(AuthorizationTestsProtectedRequest));
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    public async ValueTask DisposeAsync()
    {
        await _sut.StopAsync();
        await _sut.DisposeAsync();
    }
}

internal class TestAuthOptions : AuthenticationSchemeOptions
{
    public bool ShouldAuthenticate { get; set; }
}

internal class TestAuthHandler(IOptionsMonitor<TestAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder)
    : AuthenticationHandler<TestAuthOptions>(options, logger, encoder)
{
    public const string AuthType = "test-auth";
    public const string AuthScheme = "test-sheme";
    public const string UserName = "test-user";

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        AuthenticateResult result;
        if (Options.ShouldAuthenticate)
        {
            var claims = new[] { new Claim(ClaimTypes.Name, UserName) };
            var identity = new ClaimsIdentity(claims, AuthType);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, AuthScheme);
            result = AuthenticateResult.Success(ticket);
        }
        else
        {
            result = AuthenticateResult.NoResult();
        }
        return Task.FromResult(result);
    }
}

internal class AuthorizationTestsRequestHandler
    : IHandle<AuthorizationTestsPublicRequest>
    , IHandle<AuthorizationTestsProtectedRequest>
{
    public Task<HttpResult> Handle(AuthorizationTestsPublicRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult.Ok());

    public Task<HttpResult> Handle(AuthorizationTestsProtectedRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult.Ok());
}

[Route(nameof(AuthorizationTestsPublicRequest))]
public class AuthorizationTestsPublicRequest : IGet { }

[ProtectedRoute(nameof(AuthorizationTestsProtectedRequest))]
public class AuthorizationTestsProtectedRequest : IGet { }