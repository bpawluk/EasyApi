using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.IntegrationTests.SUT.Contract;
using BlazorUtils.EasyApi.IntegrationTests.SUT.Contract.Params;
using BlazorUtils.EasyApi.IntegrationTests.SUT.Contract.Response;
using BlazorUtils.EasyApi.IntegrationTests.SUT.Server.Params;
using BlazorUtils.EasyApi.IntegrationTests.SUT.Server.Response;
using BlazorUtils.EasyApi.IntegrationTests.SUT.Server.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorUtils.EasyApi.IntegrationTests.SUT.Server;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .Configure<AuthenticationSchemeOptions>(options => { })
            .AddSingleton<TestAuthenticationSettings>();

        builder.Services
            .AddAuthentication(TestAuthenticationSettings.Scheme)
            .AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(
                TestAuthenticationSettings.Scheme, 
                options => { });

        builder.Services.AddTransient<AuthenticationStateProvider, TestAuthenticationStateProvider>();

        builder.Services
            .AddEasyApi()
            .WithContract(
                typeof(GetRequest).Assembly,
                typeof(HeaderParamRequest).Assembly,
                typeof(NoResponseRequest).Assembly)
            .WithServer(
                typeof(ParamKindsRequestsHandler).Assembly, 
                typeof(ResponseKindsRequestsHandler).Assembly);

        var app = builder.Build();

        app.UseAuthentication();
        app.MapRequests();

        app.Run();
    }
}
