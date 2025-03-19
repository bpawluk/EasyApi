using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace BlazorUtils.EasyApi.IntegrationTests.SUT.Server.Auth;

public class TestAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options, 
    ILoggerFactory logger, 
    UrlEncoder encoder,
    TestAuthenticationSettings settings) 
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    private readonly TestAuthenticationSettings _settings = settings;

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (string.IsNullOrEmpty(_settings.UserName))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }
        else
        {
            var claims = new List<Claim>
            {
                new(TestAuthenticationSettings.NameClaim, _settings.UserName!)
            };
            var identity = new ClaimsIdentity(claims, TestAuthenticationSettings.Scheme);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, TestAuthenticationSettings.Scheme);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
