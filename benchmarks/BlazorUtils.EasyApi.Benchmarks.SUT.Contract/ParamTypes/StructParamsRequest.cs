using BlazorUtils.EasyApi.Benchmarks.SUT.Contract.Data;

namespace BlazorUtils.EasyApi.Benchmarks.SUT.Contract.ParamTypes;

[Route($"param-type/struct/{nameof(RouteParam)}")]
public class StructParamsRequest : IPost
{
    [BodyParam]
    public Guid BodyParam { get; init; }

    [HeaderParam]
    public DateTime HeaderParam { get; init; }

    [QueryStringParam]
    public Address QueryStringParam { get; init; }

    [RouteParam]
    public Address? RouteParam { get; init; }
}
