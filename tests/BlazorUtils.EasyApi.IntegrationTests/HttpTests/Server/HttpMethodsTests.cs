using Microsoft.Extensions.DependencyInjection;

namespace BlazorUtils.EasyApi.IntegrationTests.HttpTests.Server;

public class HttpMethodsTests(TestsFixture fixture) : HttpMethodsTestsBase(fixture)
{
    protected override ICall<Request> GetCaller<Request>() => _server.Services.GetRequiredService<ICall<Request>>();

    protected override ICall<Request, Response> GetCaller<Request, Response>() => _server.Services.GetRequiredService<ICall<Request, Response>>();
}
