namespace BlazorUtils.EasyApi.Tests.SUT.Contract.ParamTypes;

[Route("param-type/system/guid")]
public class GuidParamsRequest : IPost<GuidParamsRequest.Response>
{
    [HeaderParam] public Guid Guid { get; init; }
    [QueryStringParam] public Guid GuidDefault { get; init; }
    [BodyParam] public Guid? NullableGuidWithValue { get; init; }
    [HeaderParam] public Guid? NullableGuidDefault { get; init; }

    public class Response
    {
        public Guid Guid { get; init; }
        public Guid GuidDefault { get; init; }
        public Guid? NullableGuidWithValue { get; init; }
        public Guid? NullableGuidDefault { get; init; }
    }
}

[Route("param-type/system/string")]
public class StringParamsRequest : IPost<StringParamsRequest.Response>
{
    [HeaderParam] public string String { get; init; } = null!;
    [QueryStringParam] public string StringEmpty { get; init; } = null!;
    [BodyParam] public string? StringWhitespace { get; init; }
    [HeaderParam] public string? StringDefault { get; init; }

    public class Response
    {
        public string? String { get; init; }
        public string? StringEmpty { get; init; }
        public string? StringWhitespace { get; init; }
        public string? StringDefault { get; init; }
    }
}

[Route("param-type/system/uri")]
public class UriParamsRequest : IPost<UriParamsRequest.Response>
{
    [HeaderParam] public Uri HttpUri { get; init; } = null!;
    [QueryStringParam] public Uri? HttpsUri { get; init; }
    [BodyParam] public Uri FtpUri { get; init; } = null!;
    [HeaderParam] public Uri? MailtoUri { get; init; }
    [QueryStringParam] public Uri FileUri { get; init; } = null!;
    [BodyParam] public Uri? DataUri { get; init; }
    [HeaderParam] public Uri DefaultUri { get; init; } = null!;

    public class Response
    {
        public Uri HttpUri { get; init; } = null!;
        public Uri HttpsUri { get; init; } = null!;
        public Uri FtpUri { get; init; } = null!;
        public Uri MailtoUri { get; init; } = null!;
        public Uri FileUri { get; init; } = null!;
        public Uri DataUri { get; init; } = null!;
        public Uri DefaultUri { get; init; } = null!;
    }
}