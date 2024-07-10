using BlazorUtils.EasyApi.IntegrationTests.SUT.Contract.Params;

namespace BlazorUtils.EasyApi.IntegrationTests.ParamTests;

public abstract class ComplexParamsTestsBase(TestsFixture fixture) : TestsBase(fixture)
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
