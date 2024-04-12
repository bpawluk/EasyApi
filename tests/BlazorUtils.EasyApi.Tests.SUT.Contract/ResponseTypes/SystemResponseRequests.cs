namespace BlazorUtils.EasyApi.Tests.SUT.Contract.ResponseTypes;

[Route("response-type/system/guid")]
public class GuidResponseRequest : IGet<Guid>
{
    [HeaderParam] public Guid ExpectedResponse { get; init; }
}

[Route("response-type/system/nullable-guid")]
public class NullableGuidResponseRequest : IGet<Guid?>
{
    [HeaderParam] public Guid? ExpectedResponse { get; init; }
}

[Route("response-type/system/string")]
public class StringResponseRequest : IGet<string>
{
    [HeaderParam] public string ExpectedResponse { get; init; } = null!;
}

[Route("response-type/system/uri")]
public class UriResponseRequest : IGet<Uri>
{
    [HeaderParam] public Uri ExpectedResponse { get; init; } = null!;
}
