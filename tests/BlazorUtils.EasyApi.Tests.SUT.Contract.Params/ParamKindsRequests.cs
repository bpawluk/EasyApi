using BlazorUtils.EasyApi.Tests.SUT.Contract.Data;

namespace BlazorUtils.EasyApi.Tests.SUT.Contract.Params;

[Route("param-kind/no-params")]
public class NoParamsRequest : IHead { }

[Route("param-kind/header")]
public class HeaderParamRequest : IGet<HeaderParamRequest.Response>
{
    [HeaderParam]
    public int Number { get; init; }

    [HeaderParam]
    public Price Struct { get; init; }

    [HeaderParam]
    public Product Class { get; init; } = default!;

    [HeaderParam]
    public string Text { get; init; } = default!;

    [HeaderParam]
    public string? TextDefault { get; init; }

    [HeaderParam]
    public string? TextDefaultWithInitialValue { get; init; } = "value";

    public class Response
    {
        public int Number { get; init; }
        public Price Struct { get; init; }
        public Product Class { get; init; } = default!;
        public string Text { get; init; } = default!;
        public string? TextDefault { get; init; }
        public string? TextDefaultWithInitialValue { get; init; }
    }
}

[Route($"param-kind/route/{{{nameof(Number)}}}/{{{nameof(Struct)}}}/{{{nameof(Class)}}}/{{{nameof(Text)}}}")]
public class RouteParamRequest : IPost<RouteParamRequest.Response>
{
    [RouteParam]
    public int Number { get; init; }

    [RouteParam]
    public Price Struct { get; init; }

    [RouteParam]
    public Product Class { get; init; } = default!;

    [RouteParam]
    public string Text { get; init; } = default!;

    public class Response
    {
        public int Number { get; init; }
        public Price Struct { get; init; }
        public Product Class { get; init; } = default!;
        public string Text { get; init; } = default!;
    }
}

[Route("param-kind/query-string")]
public class QueryStringParamRequest : IPut<QueryStringParamRequest.Response>
{
    [QueryStringParam]
    public int Number { get; init; }

    [QueryStringParam]
    public Price Struct { get; init; }

    [QueryStringParam]
    public Product Class { get; init; } = default!;

    [QueryStringParam]
    public string Text { get; init; } = default!;

    [QueryStringParam]
    public string? TextDefault { get; init; }

    [QueryStringParam]
    public string? TextDefaultWithInitialValue { get; init; } = "value";

    public class Response
    {
        public int Number { get; init; }
        public Price Struct { get; init; }
        public Product Class { get; init; } = default!;
        public string Text { get; init; } = default!;
        public string? TextDefault { get; init; }
        public string? TextDefaultWithInitialValue { get; init; }
    }
}

[Route("param-kind/body")]
public class BodyParamRequest : IDelete<BodyParamRequest.Response>
{
    [BodyParam]
    public int Number { get; init; }

    [BodyParam]
    public Price Struct { get; init; }

    [BodyParam]
    public Product Class { get; init; } = default!;

    [BodyParam]
    public string Text { get; init; } = default!;

    [BodyParam]
    public string? TextDefault { get; init; }

    [BodyParam]
    public string? TextDefaultWithInitialValue { get; init; } = "value";

    public class Response
    {
        public int Number { get; init; }
        public Price Struct { get; init; }
        public Product Class { get; init; } = default!;
        public string Text { get; init; } = default!;
        public string? TextDefault { get; init; }
        public string? TextDefaultWithInitialValue { get; init; }
    }
}