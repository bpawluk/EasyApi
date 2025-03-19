using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorUtils.EasyApi.IntegrationTests.SUT.Server.Auth;

public class TestAuthenticationStateProvider(TestAuthenticationSettings settings) : AuthenticationStateProvider
{
    private readonly TestAuthenticationSettings _settings = settings;

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (string.IsNullOrEmpty(_settings.UserName))
        {
            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));
        }
        else
        {
            var claims = new List<Claim>
            {
                new(TestAuthenticationSettings.NameClaim, _settings.UserName!)
            };
            var identity = new ClaimsIdentity(claims, TestAuthenticationSettings.Scheme);
            var principal = new ClaimsPrincipal(identity);
            return Task.FromResult(new AuthenticationState(principal));
        }
    }
}
