namespace BlazorUtils.EasyApi.Tests.SUT.Contract.ResponseTypes;

[Route("response-type/enum")]
public class EnumResponseRequest : IGet<Time> 
{
    [HeaderParam] public Time ExpectedResponse { get; init; }
}

[Route("response-type/nullable-enum")]
public class NullableEnumResponseRequest : IGet<Time?>
{
    [HeaderParam] public Time? ExpectedResponse { get; init; }
}

public enum Time
{
    Day,
    Night
}
