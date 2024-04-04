using Microsoft.Extensions.DependencyInjection;

namespace BlazorUtils.EasyApi.Tests.ResponseTests;

public abstract class ResponseTypesTests(TestsFixture fixture) : TestsBase(fixture)
{

}

public class Client_ResponseTypesTests(TestsFixture fixture) : ResponseTypesTests(fixture)
{
    protected override ICall<Request> GetCaller<Request>() => _client.Services.GetRequiredService<ICall<Request>>();

    protected override ICall<Request, Response> GetCaller<Request, Response>() => _client.Services.GetRequiredService<ICall<Request, Response>>();
}

public class Server_ResponseTypesTests(TestsFixture fixture) : ResponseTypesTests(fixture)
{
    protected override ICall<Request> GetCaller<Request>() => _server.Services.GetRequiredService<ICall<Request>>();

    protected override ICall<Request, Response> GetCaller<Request, Response>() => _server.Services.GetRequiredService<ICall<Request, Response>>();
}
