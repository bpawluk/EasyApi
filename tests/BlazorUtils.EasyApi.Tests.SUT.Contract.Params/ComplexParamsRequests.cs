using BlazorUtils.EasyApi.Tests.SUT.Contract.Data;

namespace BlazorUtils.EasyApi.Tests.SUT.Contract.Params;

[Route("param-type/complex")]
public class ComplexParamsRequest : IPost<ComplexParamsRequest.Response>
{
    [HeaderParam] public Price Struct { get; init; }
    [QueryStringParam] public Price StructDefault { get; init; }
    [BodyParam] public Price? NullableStructWithValue { get; init; }
    [HeaderParam] public Price? NullableStructDefault { get; init; }

    [HeaderParam] public Product Class { get; init; } = null!;
    [QueryStringParam] public Product? ClassDefault { get; init; }

    public class Response
    {
        public Price Struct { get; init; }
        public Price StructDefault { get; init; }
        public Price? NullableStructWithValue { get; init; }
        public Price? NullableStructDefault { get; init; }

        public Product Class { get; init; } = null!;
        public Product? ClassDefault { get; init; }
    }
}