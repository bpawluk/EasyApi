using Microsoft.Extensions.DependencyInjection;

namespace BlazorUtils.EasyApi.IntegrationTests.ResponseTests.Client;

public class EnumResponseTests(TestsFixture fixture) : EnumResponseTestsBase(fixture)
{
    protected override ICall<Request> GetCaller<Request>() => _client.Services.GetRequiredService<ICall<Request>>();

    protected override ICall<Request, Response> GetCaller<Request, Response>() => _client.Services.GetRequiredService<ICall<Request, Response>>();
}
