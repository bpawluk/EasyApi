using BlazorUtils.EasyApi.Tests.SUT.Contract.ParamTypes;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorUtils.EasyApi.Tests.ParamTests.ParamTypesTests;

public abstract class ComplexParamsTests(TestsFixture fixture) : TestsBase(fixture)
{
    [Fact]
    public async Task Request_WithComplexParams()
    {
        var request = new ComplexParamsRequest()
        {
            Struct = new() { Amount = 9.99M, Currency = "USD" },
            StructDefault = default,
            NullableStructWithValue = new() { Amount = 1M, Currency = "PLN" },
            NullableStructDefault = null,
            Class = new() { Name = "T-Shirt", Price = 9.99, StockQuantity = 100, CreatedAt = DateTime.UtcNow },
            ClassDefault = null,
        };

        var result = await CallHttp<ComplexParamsRequest, ComplexParamsRequest.Response>(request);

        Assert.Equal(request.Struct, result.Struct);
        Assert.Equal(request.StructDefault, result.StructDefault);
        Assert.Equal(request.NullableStructWithValue, result.NullableStructWithValue);
        Assert.Equal(request.NullableStructDefault, result.NullableStructDefault);
        Assert.Equal(request.Class, result.Class);
        Assert.Equal(request.ClassDefault, result.ClassDefault);
    }
}

public class Client_ComplexParamsTests(TestsFixture fixture) : ComplexParamsTests(fixture)
{
    protected override ICall<Request> GetCaller<Request>() => _client.Services.GetRequiredService<ICall<Request>>();

    protected override ICall<Request, Response> GetCaller<Request, Response>() => _client.Services.GetRequiredService<ICall<Request, Response>>();
}

public class Server_ComplexParamsTests(TestsFixture fixture) : ComplexParamsTests(fixture)
{
    protected override ICall<Request> GetCaller<Request>() => _server.Services.GetRequiredService<ICall<Request>>();

    protected override ICall<Request, Response> GetCaller<Request, Response>() => _server.Services.GetRequiredService<ICall<Request, Response>>();
}