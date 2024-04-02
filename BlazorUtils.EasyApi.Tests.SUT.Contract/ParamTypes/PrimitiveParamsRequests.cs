namespace BlazorUtils.EasyApi.Tests.SUT.Contract.ParamTypes;

[Route("param-type/primitive/integral")]
public class IntegralParamsRequest : IPost<IntegralParamsRequest.Response>
{
    [HeaderParam] public sbyte SignedByte { get; init; }
    [HeaderParam] public sbyte SignedByteDefault { get; init; }
    [HeaderParam] public sbyte? NullableSignedByteWithValue { get; init; }
    [HeaderParam] public sbyte? NullableSignedByteDefault { get; init; }

    [QueryStringParam] public byte Byte { get; init; }
    [QueryStringParam] public byte ByteDefault { get; init; }
    [QueryStringParam] public byte? NullableByteWithValue { get; init; }
    [QueryStringParam] public byte? NullableByteDefault { get; init; }

    [BodyParam] public short Short { get; init; }
    [BodyParam] public short ShortDefault { get; init; }
    [BodyParam] public short? NullableShortWithValue { get; init; }
    [BodyParam] public short? NullableShortDefault { get; init; }

    [HeaderParam] public ushort UnsignedShort { get; init; }
    [HeaderParam] public ushort UnsignedShortDefault { get; init; }
    [HeaderParam] public ushort? NullableUnsignedShortWithValue { get; init; }
    [HeaderParam] public ushort? NullableUnsignedShortDefault { get; init; }

    [QueryStringParam] public int Integer { get; init; }
    [QueryStringParam] public int IntegerDefault { get; init; }
    [QueryStringParam] public int? NullableIntegerWithValue { get; init; }
    [QueryStringParam] public int? NullableIntegerDefault { get; init; }

    [BodyParam] public uint UnsignedInteger { get; init; }
    [BodyParam] public uint UnsignedIntegerDefault { get; init; }
    [BodyParam] public uint? NullableUnsignedIntegerWithValue { get; init; }
    [BodyParam] public uint? NullableUnsignedIntegerDefault { get; init; }

    [HeaderParam] public long Long { get; init; }
    [HeaderParam] public long LongDefault { get; init; }
    [HeaderParam] public long? NullableLongWithValue { get; init; }
    [HeaderParam] public long? NullableLongDefault { get; init; }

    [QueryStringParam] public ulong UnsignedLong { get; init; }
    [QueryStringParam] public ulong UnsignedLongDefault { get; init; }
    [QueryStringParam] public ulong? NullableUnsignedLongWithValue { get; init; }
    [QueryStringParam] public ulong? NullableUnsignedLongDefault { get; init; }

    public class Response
    {
        public sbyte SignedByte { get; init; }
        public sbyte SignedByteDefault { get; init; }
        public sbyte? NullableSignedByteWithValue { get; init; }
        public sbyte? NullableSignedByteDefault { get; init; }

        public byte Byte { get; init; }
        public byte ByteDefault { get; init; }
        public byte? NullableByteWithValue { get; init; }
        public byte? NullableByteDefault { get; init; }

        public short Short { get; init; }
        public short ShortDefault { get; init; }
        public short? NullableShortWithValue { get; init; }
        public short? NullableShortDefault { get; init; }

        public ushort UnsignedShort { get; init; }
        public ushort UnsignedShortDefault { get; init; }
        public ushort? NullableUnsignedShortWithValue { get; init; }
        public ushort? NullableUnsignedShortDefault { get; init; }

        public int Integer { get; init; }
        public int IntegerDefault { get; init; }
        public int? NullableIntegerWithValue { get; init; }
        public int? NullableIntegerDefault { get; init; }

        public uint UnsignedInteger { get; init; }
        public uint UnsignedIntegerDefault { get; init; }
        public uint? NullableUnsignedIntegerWithValue { get; init; }
        public uint? NullableUnsignedIntegerDefault { get; init; }

        public long Long { get; init; }
        public long LongDefault { get; init; }
        public long? NullableLongWithValue { get; init; }
        public long? NullableLongDefault { get; init; }

        public ulong UnsignedLong { get; init; }
        public ulong UnsignedLongDefault { get; init; }
        public ulong? NullableUnsignedLongWithValue { get; init; }
        public ulong? NullableUnsignedLongDefault { get; init; }
    }
}

[Route("param-type/primitive/floating")]
public class FloatingParamsRequest : IPost<FloatingParamsRequest.Response>
{
    [HeaderParam] public float Float { get; init; }
    [HeaderParam] public float FloatDefault { get; init; }
    [HeaderParam] public float? NullableFloatWithValue { get; init; }
    [HeaderParam] public float? NullableFloatDefault { get; init; }

    [QueryStringParam] public double Double { get; init; }
    [QueryStringParam] public double DoubleDefault { get; init; }
    [QueryStringParam] public double? NullableDoubleWithValue { get; init; }
    [QueryStringParam] public double? NullableDoubleDefault { get; init; }

    [BodyParam] public decimal Decimal { get; init; }
    [BodyParam] public decimal DecimalDefault { get; init; }
    [BodyParam] public decimal? NullableDecimalWithValue { get; init; }
    [BodyParam] public decimal? NullableDecimalDefault { get; init; }

    public class Response
    {
        public float Float { get; init; }
        public float FloatDefault { get; init; }
        public float? NullableFloatWithValue { get; init; }
        public float? NullableFloatDefault { get; init; }

        public double Double { get; init; }
        public double DoubleDefault { get; init; }
        public double? NullableDoubleWithValue { get; init; }
        public double? NullableDoubleDefault { get; init; }

        public decimal Decimal { get; init; }
        public decimal DecimalDefault { get; init; }
        public decimal? NullableDecimalWithValue { get; init; }
        public decimal? NullableDecimalDefault { get; init; }
    }
}

[Route("param-type/primitive/boolean")]
public class BooleanParamsRequest : IPost<BooleanParamsRequest.Response>
{
    [HeaderParam] public bool Bool { get; init; }
    [HeaderParam] public bool BoolDefault { get; init; }
    [HeaderParam] public bool? NullableBoolWithValue { get; init; }
    [HeaderParam] public bool? NullableBoolDefault { get; init; }

    public class Response
    {
        public bool Bool { get; init; }
        public bool BoolDefault { get; init; }
        public bool? NullableBoolWithValue { get; init; }
        public bool? NullableBoolDefault { get; init; }
    }
}

[Route("param-type/primitive/character")]
public class CharacterParamsRequest : IPost<CharacterParamsRequest.Response>
{
    [HeaderParam] public char Char { get; init; }
    [HeaderParam] public char CharDefault { get; init; }
    [HeaderParam] public char? NullableCharWithValue { get; init; }
    [HeaderParam] public char? NullableCharDefault { get; init; }

    public class Response
    {
        public char Char { get; init; }
        public char CharDefault { get; init; }
        public char? NullableCharWithValue { get; init; }
        public char? NullableCharDefault { get; init; }
    }
}
