using BlazorUtils.EasyApi.IntegrationTests.SUT.Contract.Response;
using System.Net;

namespace BlazorUtils.EasyApi.IntegrationTests.ResponseTests;

public abstract class PrimitiveResponseTestsBase(TestsFixture fixture) : TestsBase(fixture)
{
    [Theory]
    [InlineData(int.MinValue)]
    [InlineData(0)]
    [InlineData(int.MaxValue)]
    public async Task Response_OfIntegralType(int expectedResponse)
    {
        var request = new IntegralResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<IntegralResponseRequest, int>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Theory]
    [InlineData(int.MinValue)]
    [InlineData(0)]
    [InlineData(int.MaxValue)]
    public async Task Response_OfNullableIntegralType_WithValue(int? expectedResponse)
    {
        var request = new NullableIntegralResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<NullableIntegralResponseRequest, int?>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Fact]
    public async Task Response_OfNullableIntegralType_NoValue()
    {
        var request = new NullableIntegralResponseRequest() { ExpectedResponse = null };
        var caller = GetCaller<NullableIntegralResponseRequest, int?>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }

    [Theory]
    [InlineData(float.MinValue)]
    [InlineData(0f)]
    [InlineData(float.MaxValue)]
    public async Task Response_OfFloatingType(float expectedResponse)
    {
        var request = new FloatingResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<FloatingResponseRequest, float>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Theory]
    [InlineData(float.MinValue)]
    [InlineData(0f)]
    [InlineData(float.MaxValue)]
    public async Task Response_OfNullableFloatingType_WithValue(float? expectedResponse)
    {
        var request = new NullableFloatingResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<NullableFloatingResponseRequest, float?>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Fact]
    public async Task Response_OfNullableFloatingType_NoValue()
    {
        var request = new NullableFloatingResponseRequest() { ExpectedResponse = null };
        var caller = GetCaller<NullableFloatingResponseRequest, float?>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task Response_OfBooleanType(bool expectedResponse)
    {
        var request = new BooleanResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<BooleanResponseRequest, bool>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task Response_OfNullableBooleanType_WithValue(bool? expectedResponse)
    {
        var request = new NullableBooleanResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<NullableBooleanResponseRequest, bool?>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Fact]
    public async Task Response_OfNullableBooleanType_NoValue()
    {
        var request = new NullableBooleanResponseRequest() { ExpectedResponse = null };
        var caller = GetCaller<NullableBooleanResponseRequest, bool?>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }

    [Theory]
    [InlineData('\0')]
    [InlineData('x')]
    [InlineData('\x006A')]
    public async Task Response_OfCharacterType(char expectedResponse)
    {
        var request = new CharacterResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<CharacterResponseRequest, char>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Theory]
    [InlineData('\0')]
    [InlineData('x')]
    [InlineData('\x006A')]
    public async Task Response_OfNullableCharacterType_WithValue(char? expectedResponse)
    {
        var request = new NullableCharacterResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<NullableCharacterResponseRequest, char?>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Fact]
    public async Task Response_OfNullableCharacterType_NoValue()
    {
        var request = new NullableCharacterResponseRequest() { ExpectedResponse = null };
        var caller = GetCaller<NullableCharacterResponseRequest, char?>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }
}
