using BlazorUtils.EasyApi.Benchmarks.SUT.Contract.Data;

namespace BlazorUtils.EasyApi.Benchmarks.SUT.Contract.ParamTypes;

[Route($"param-type/class/{nameof(RouteParam)}")]
public class ClassParamsRequest : IPost
{
    [BodyParam]
    public Product BodyParam { get; init; } = null!;

    [HeaderParam]
    public Customer HeaderParam { get; init; } = null!;

    [QueryStringParam]
    public Person QueryStringParam { get; init; } = null!;

    [RouteParam]
    public Person? RouteParam { get; init; }
}
