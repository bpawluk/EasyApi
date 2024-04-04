using BlazorUtils.EasyApi.Tests.SUT.Contract.ParamTypes;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorUtils.EasyApi.Tests.ParamTests.ParamTypesTests;

public abstract class EnumParamsTests(TestsFixture fixture) : TestsBase(fixture)
{
    [Fact]
    public async Task Request_WithEnumParams()
    {
        var request = new EnumParamsRequest()
        {
            Enum = Season.Summer,
            EnumDefault = default,
            NullableEnumWithValue = Season.Autumn,
            NullableEnumDefault = null
        };

        var result = await CallHttp<EnumParamsRequest, EnumParamsRequest.Response>(request);

        Assert.Equal(request.Enum, result.Enum);
        Assert.Equal(request.EnumDefault, result.EnumDefault);
        Assert.Equal(request.NullableEnumWithValue, result.NullableEnumWithValue);
        Assert.Equal(request.NullableEnumDefault, result.NullableEnumDefault);
    }
}

public class Client_EnumParamsTests(TestsFixture fixture) : EnumParamsTests(fixture)
{
    protected override ICall<Request> GetCaller<Request>() => _client.Services.GetRequiredService<ICall<Request>>();

    protected override ICall<Request, Response> GetCaller<Request, Response>() => _client.Services.GetRequiredService<ICall<Request, Response>>();
}

public class Server_EnumParamsTests(TestsFixture fixture) : EnumParamsTests(fixture)
{
    protected override ICall<Request> GetCaller<Request>() => _server.Services.GetRequiredService<ICall<Request>>();

    protected override ICall<Request, Response> GetCaller<Request, Response>() => _server.Services.GetRequiredService<ICall<Request, Response>>();
}
