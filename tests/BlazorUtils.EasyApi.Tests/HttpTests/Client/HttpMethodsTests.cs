using Microsoft.Extensions.DependencyInjection;

namespace BlazorUtils.EasyApi.Tests.HttpTests.Client;

public class HttpMethodsTests(TestsFixture fixture) : HttpMethodsTestsBase(fixture)
{
    protected override ICall<Request> GetCaller<Request>() => _client.Services.GetRequiredService<ICall<Request>>();

    protected override ICall<Request, Response> GetCaller<Request, Response>() => _client.Services.GetRequiredService<ICall<Request, Response>>();
}
