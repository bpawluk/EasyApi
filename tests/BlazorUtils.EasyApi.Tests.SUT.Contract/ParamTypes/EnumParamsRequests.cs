namespace BlazorUtils.EasyApi.Tests.SUT.Contract.ParamTypes;

[Route("param-type/enum")]
public class EnumParamsRequest : IPost<EnumParamsRequest.Response>
{
    [HeaderParam] public Season Enum { get; init; }
    [QueryStringParam] public Season EnumDefault { get; init; }
    [BodyParam] public Season? NullableEnumWithValue { get; init; }
    [HeaderParam] public Season? NullableEnumDefault { get; init; }

    public class Response
    {
        public Season Enum { get; init; }
        public Season EnumDefault { get; init; }
        public Season? NullableEnumWithValue { get; init; }
        public Season? NullableEnumDefault { get; init; }
    }
}

public enum Season
{
    Spring,
    Summer,
    Autumn,
    Winter
}
