using BlazorUtils.EasyApi.Tests.SUT.Contract.Response;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace BlazorUtils.EasyApi.Tests.ResponseTests;

public abstract class EnumResponseTestsBase(TestsFixture fixture) : TestsBase(fixture)
{
    [Fact]
    public async Task Response_OfEnumType()
    {
        var request = new EnumResponseRequest() { ExpectedResponse = Time.Day };
        var result = await CallHttp<EnumResponseRequest, Time>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Fact]
    public async Task Response_OfNullableEnumType_WithValue()
    {
        var request = new NullableEnumResponseRequest() { ExpectedResponse = Time.Night };
        var result = await CallHttp<NullableEnumResponseRequest, Time?>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Fact]
    public async Task Response_OfNullableEnumType_NoValue()
    {
        var request = new NullableEnumResponseRequest() { ExpectedResponse = null };
        var caller = GetCaller<NullableEnumResponseRequest, Time?>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }
}
