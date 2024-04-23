namespace BlazorUtils.EasyApi.Benchmarks.SUT.Contract.ParamTypes;

[Route($"param-type/primitive/{nameof(RouteParam)}")]
public class PrimitiveParamsRequest : IPost
{
    [BodyParam]
    public int BodyParam { get; init; }

    [HeaderParam]
    public float HeaderParam { get; init; }

    [QueryStringParam]
    public decimal QueryStringParam { get; init; }

    [RouteParam]
    public decimal? RouteParam { get; init; }
}
