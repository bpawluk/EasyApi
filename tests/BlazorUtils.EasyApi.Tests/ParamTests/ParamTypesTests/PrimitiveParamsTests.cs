using BlazorUtils.EasyApi.Tests.SUT.Contract.ParamTypes;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorUtils.EasyApi.Tests.ParamTests.ParamTypesTests;

public abstract class PrimitiveParamsTests(TestsFixture fixture) : TestsBase(fixture)
{
    [Fact]
    public async Task Request_WithPrimitiveParams_IntegralNumbers()
    {
        var request = new IntegralParamsRequest()
        {
            SignedByte = -1,
            SignedByteDefault = 0,
            NullableSignedByteWithValue = 1,
            NullableSignedByteDefault = null,
            Byte = 10,
            ByteDefault = 0,
            NullableByteWithValue = 10,
            NullableByteDefault = null,
            Short = -100,
            ShortDefault = 0,
            NullableShortWithValue = 100,
            NullableShortDefault = null,
            UnsignedShort = 1000,
            UnsignedShortDefault = 0,
            NullableUnsignedShortWithValue = 1000,
            NullableUnsignedShortDefault = null,
            Integer = -10000,
            IntegerDefault = 0,
            NullableIntegerWithValue = 10000,
            NullableIntegerDefault = null,
            UnsignedInteger = 100000,
            UnsignedIntegerDefault = 0,
            NullableUnsignedIntegerWithValue = 100000,
            NullableUnsignedIntegerDefault = null,
            Long = -1000000,
            LongDefault = 0,
            NullableLongWithValue = 1000000,
            NullableLongDefault = null,
            UnsignedLong = 10000000,
            UnsignedLongDefault = 0,
            NullableUnsignedLongWithValue = 10000000,
            NullableUnsignedLongDefault = null
        };

        var result = await CallHttp<IntegralParamsRequest, IntegralParamsRequest.Response>(request);

        Assert.Equal(request.SignedByte, result.SignedByte);
        Assert.Equal(request.SignedByteDefault, result.SignedByteDefault);
        Assert.Equal(request.NullableSignedByteWithValue, result.NullableSignedByteWithValue);
        Assert.Equal(request.NullableSignedByteDefault, result.NullableSignedByteDefault);
        Assert.Equal(request.Byte, result.Byte);
        Assert.Equal(request.ByteDefault, result.ByteDefault);
        Assert.Equal(request.NullableByteWithValue, result.NullableByteWithValue);
        Assert.Equal(request.NullableByteDefault, result.NullableByteDefault);
        Assert.Equal(request.Short, result.Short);
        Assert.Equal(request.ShortDefault, result.ShortDefault);
        Assert.Equal(request.NullableShortWithValue, result.NullableShortWithValue);
        Assert.Equal(request.NullableShortDefault, result.NullableShortDefault);
        Assert.Equal(request.UnsignedShort, result.UnsignedShort);
        Assert.Equal(request.UnsignedShortDefault, result.UnsignedShortDefault);
        Assert.Equal(request.NullableUnsignedShortWithValue, result.NullableUnsignedShortWithValue);
        Assert.Equal(request.NullableUnsignedShortDefault, result.NullableUnsignedShortDefault);
        Assert.Equal(request.Integer, result.Integer);
        Assert.Equal(request.IntegerDefault, result.IntegerDefault);
        Assert.Equal(request.NullableIntegerWithValue, result.NullableIntegerWithValue);
        Assert.Equal(request.NullableIntegerDefault, result.NullableIntegerDefault);
        Assert.Equal(request.UnsignedInteger, result.UnsignedInteger);
        Assert.Equal(request.UnsignedIntegerDefault, result.UnsignedIntegerDefault);
        Assert.Equal(request.NullableUnsignedIntegerWithValue, result.NullableUnsignedIntegerWithValue);
        Assert.Equal(request.NullableUnsignedIntegerDefault, result.NullableUnsignedIntegerDefault);
        Assert.Equal(request.Long, result.Long);
        Assert.Equal(request.LongDefault, result.LongDefault);
        Assert.Equal(request.NullableLongWithValue, result.NullableLongWithValue);
        Assert.Equal(request.NullableLongDefault, result.NullableLongDefault);
        Assert.Equal(request.UnsignedLong, result.UnsignedLong);
        Assert.Equal(request.UnsignedLongDefault, result.UnsignedLongDefault);
        Assert.Equal(request.NullableUnsignedLongWithValue, result.NullableUnsignedLongWithValue);
        Assert.Equal(request.NullableUnsignedLongDefault, result.NullableUnsignedLongDefault);
    }

    [Fact]
    public async Task Request_WithPrimitiveParams_FloatingPointNumbers()
    {
        var request = new FloatingParamsRequest()
        {
            Float = -1.11F,
            FloatDefault = 0,
            NullableFloatWithValue = 1.11F,
            NullableFloatDefault = null,
            Double = -2.22D,
            DoubleDefault = 0,
            NullableDoubleWithValue = 2.22D,
            NullableDoubleDefault = null,
            Decimal = -3.33M,
            DecimalDefault = 0,
            NullableDecimalWithValue = 3.33M,
            NullableDecimalDefault = null
        };

        var result = await CallHttp<FloatingParamsRequest, FloatingParamsRequest.Response>(request);

        Assert.Equal(request.Float, result.Float);
        Assert.Equal(request.FloatDefault, result.FloatDefault);
        Assert.Equal(request.NullableFloatWithValue, result.NullableFloatWithValue);
        Assert.Equal(request.NullableFloatDefault, result.NullableFloatDefault);
        Assert.Equal(request.Double, result.Double);
        Assert.Equal(request.DoubleDefault, result.DoubleDefault);
        Assert.Equal(request.NullableDoubleWithValue, result.NullableDoubleWithValue);
        Assert.Equal(request.NullableDoubleDefault, result.NullableDoubleDefault);
        Assert.Equal(request.Decimal, result.Decimal);
        Assert.Equal(request.DecimalDefault, result.DecimalDefault);
        Assert.Equal(request.NullableDecimalWithValue, result.NullableDecimalWithValue);
        Assert.Equal(request.NullableDecimalDefault, result.NullableDecimalDefault);
    }

    [Fact]
    public async Task Request_WithPrimitiveParams_Booleans()
    {
        var request = new BooleanParamsRequest()
        {
            Bool = true,
            BoolDefault = false,
            NullableBoolWithValue = true,
            NullableBoolDefault = null
        };

        var result = await CallHttp<BooleanParamsRequest, BooleanParamsRequest.Response>(request);

        Assert.Equal(request.Bool, result.Bool);
        Assert.Equal(request.BoolDefault, result.BoolDefault);
        Assert.Equal(request.NullableBoolWithValue, result.NullableBoolWithValue);
        Assert.Equal(request.NullableBoolDefault, result.NullableBoolDefault);
    }

    [Fact]
    public async Task Request_WithPrimitiveParams_Characters()
    {
        var request = new CharacterParamsRequest()
        {
            Char = 'x',
            CharDefault = '\0',
            NullableCharWithValue = '\x006A',
            NullableCharDefault = null
        };

        var result = await CallHttp<CharacterParamsRequest, CharacterParamsRequest.Response>(request);

        Assert.Equal(request.Char, result.Char);
        Assert.Equal(request.CharDefault, result.CharDefault);
        Assert.Equal(request.NullableCharWithValue, result.NullableCharWithValue);
        Assert.Equal(request.NullableCharDefault, result.NullableCharDefault);
    }
}

public class Client_PrimitiveParamsTests(TestsFixture fixture) : PrimitiveParamsTests(fixture)
{
    protected override ICall<Request> GetCaller<Request>() => _client.Services.GetRequiredService<ICall<Request>>();

    protected override ICall<Request, Response> GetCaller<Request, Response>() => _client.Services.GetRequiredService<ICall<Request, Response>>();
}

public class Server_PrimitiveParamsTests(TestsFixture fixture) : PrimitiveParamsTests(fixture)
{
    protected override ICall<Request> GetCaller<Request>() => _server.Services.GetRequiredService<ICall<Request>>();

    protected override ICall<Request, Response> GetCaller<Request, Response>() => _server.Services.GetRequiredService<ICall<Request, Response>>();
}
