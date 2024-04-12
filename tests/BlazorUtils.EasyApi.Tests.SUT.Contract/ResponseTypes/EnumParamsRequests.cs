namespace BlazorUtils.EasyApi.Tests.SUT.Contract.ResponseTypes;

[Route("response-type/enum")]
public class EnumResponseRequest : IGet<Time> { }

[Route("response-type/nullable-enum")]
public class NullableEnumResponseRequest : IGet<Time?>
{
    [HeaderParam] public bool ExpectValue { get; init; }
}

public enum Time
{
    Day,
    Night
}
