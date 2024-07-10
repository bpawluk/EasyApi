using BlazorUtils.EasyApi.IntegrationTests.SUT.Contract.Data;
using BlazorUtils.EasyApi.IntegrationTests.SUT.Contract.Response;
using System.Net;

namespace BlazorUtils.EasyApi.IntegrationTests.ResponseTests;

public abstract class ComplexResponseTestsBase(TestsFixture fixture) : TestsBase(fixture)
{
    public static TheoryData<Price> StructValues => new()
    {
        new(),
        new() { Amount = 9.99M, Currency = "USD" }
    };

    public static TheoryData<Product> ClassValues => new()
    {
        new(),
        new() { Name = "T-Shirt", Price = 9.99, StockQuantity = 100, CreatedAt = DateTime.UtcNow }
    };

    [Theory]
    [MemberData(nameof(StructValues))]
    public async Task Response_OfStructType(Price expectedResponse)
    {
        var request = new StructResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<StructResponseRequest, Price>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Theory]
    [MemberData(nameof(StructValues))]
    public async Task Response_OfNullableStructType_WithValue(Price expectedResponse)
    {
        var request = new NullableStructResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<NullableStructResponseRequest, Price?>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Fact]
    public async Task Response_OfNullableStructType_NoValue()
    {
        var request = new NullableStructResponseRequest() { ExpectedResponse = null };
        var caller = GetCaller<NullableStructResponseRequest, Price?>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }

    [Theory]
    [MemberData(nameof(ClassValues))]
    public async Task Response_OfClassType_WithValuee(Product expectedResponse)
    {
        var request = new ClassResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<ClassResponseRequest, Product>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Fact]
    public async Task Response_OfClassType_NoValue()
    {
        var request = new ClassResponseRequest() { ExpectedResponse = null! };
        var caller = GetCaller<ClassResponseRequest, Product?>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }
}
