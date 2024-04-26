using BlazorUtils.EasyApi.Tests.SUT.Contract.Data;

namespace BlazorUtils.EasyApi.Tests.SUT.Contract.Params;

[Route("param-kind/no-params")]
public class NoParamsRequest : IGet { }

[Route("param-kind/header")]
public class HeaderParamRequest : IHead
{
    [HeaderParam]
    public int Number { get; init; }

    [HeaderParam]
    public string Text { get; init; } = default!;

    [HeaderParam]
    public Price Struct { get; init; }

    [HeaderParam]
    public Product Class { get; init; } = default!;
}

[Route($"param-kind/route/{{{nameof(Number)}}}/{{{nameof(Text)}}}/{{{nameof(Struct)}}}/{{{nameof(Class)}}}")]
public class RouteParamRequest : IGet
{
    [RouteParam]
    public int Number { get; init; }

    [RouteParam]
    public string Text { get; init; } = default!;

    [RouteParam]
    public Price Struct { get; init; }

    [RouteParam]
    public Product Class { get; init; } = default!;
}

[Route("param-kind/query-string")]
public class QueryStringParamRequest : IDelete
{
    [QueryStringParam]
    public int Number { get; init; }

    [QueryStringParam]
    public string Text { get; init; } = default!;

    [QueryStringParam]
    public Price Struct { get; init; }

    [QueryStringParam]
    public Product Class { get; init; } = default!;
}

[Route("param-kind/body")]
public class BodyParamRequest : IPost
{
    [BodyParam]
    public int Number { get; init; }

    [BodyParam]
    public string Text { get; init; } = default!;

    [BodyParam]
    public Price Struct { get; init; }

    [BodyParam]
    public Product Class { get; init; } = default!;
}