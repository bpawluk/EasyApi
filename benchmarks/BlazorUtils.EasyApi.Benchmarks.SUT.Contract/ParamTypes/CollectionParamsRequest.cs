using BlazorUtils.EasyApi.Benchmarks.SUT.Contract.Data;

namespace BlazorUtils.EasyApi.Benchmarks.SUT.Contract.ParamTypes;

[Route($"param-type/collection/{nameof(RouteParam)}")]
public class CollectionParamsRequest : IPost
{
    [BodyParam]
    public IEnumerable<int> BodyParam { get; init; } = null!;

    [HeaderParam]
    public IList<string> HeaderParam { get; init; } = null!;

    [QueryStringParam]
    public Stack<Address> QueryStringParam { get; init; } = null!;

    [RouteParam]
    public Queue<Customer> RouteParam { get; init; } = null!;
}
