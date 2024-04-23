namespace BlazorUtils.EasyApi.Benchmarks.SUT.Contract.ParamTypes;

[Route($"param-type/text/{nameof(RouteParam)}")]
public class TextParamsRequest : IPost
{
    [BodyParam]
    public string BodyParam { get; init; } = null!;

    [HeaderParam]
    public string? HeaderParam { get; init; }

    [QueryStringParam]
    public char QueryStringParam { get; init; }

    [RouteParam]
    public char? RouteParam { get; init; }
}
