using Microsoft.Extensions.DependencyInjection;

namespace BlazorUtils.EasyApi.IntegrationTests.ParamTests.Client;

public class EnumParamsTests(TestsFixture fixture) : EnumParamsTestsBase(fixture)
{
    protected override ICall<Request> GetCaller<Request>() => _client.Services.GetRequiredService<ICall<Request>>();

    protected override ICall<Request, Response> GetCaller<Request, Response>() => _client.Services.GetRequiredService<ICall<Request, Response>>();
}
