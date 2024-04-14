using BlazorUtils.EasyApi.Tests.SUT.Contract.Data;
using BlazorUtils.EasyApi.Tests.SUT.Contract.ResponseTypes;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace BlazorUtils.EasyApi.Tests.ResponseTests.ResponseTypesTests;

public abstract class ComplexResponseTests(TestsFixture fixture) : TestsBase(fixture)
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
    public async Task Request_ForResponse_OfStructType(Price expectedResponse)
    {
        var request = new StructResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<StructResponseRequest, Price>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Theory]
    [MemberData(nameof(StructValues))]
    public async Task Request_ForResponse_OfNullableStructType_WithValue(Price expectedResponse)
    {
        var request = new NullableStructResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<NullableStructResponseRequest, Price?>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Fact]
    public async Task Request_ForResponse_OfNullableStructType_NoValue()
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
    public async Task Request_ForResponse_OfClassType_WithValuee(Product expectedResponse)
    {
        var request = new ClassResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<ClassResponseRequest, Product>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Fact]
    public async Task Request_ForResponse_OfClassType_NoValue()
    {
        var request = new ClassResponseRequest() { ExpectedResponse = null! };
        var caller = GetCaller<ClassResponseRequest, Product?>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }
}

public class Client_ComplexResponseTests(TestsFixture fixture) : ComplexResponseTests(fixture)
{
    protected override ICall<Request> GetCaller<Request>() => _client.Services.GetRequiredService<ICall<Request>>();

    protected override ICall<Request, Response> GetCaller<Request, Response>() => _client.Services.GetRequiredService<ICall<Request, Response>>();
}

public class Server_ComplexResponseTests(TestsFixture fixture) : ComplexResponseTests(fixture)
{
    protected override ICall<Request> GetCaller<Request>() => _server.Services.GetRequiredService<ICall<Request>>();

    protected override ICall<Request, Response> GetCaller<Request, Response>() => _server.Services.GetRequiredService<ICall<Request, Response>>();
}
