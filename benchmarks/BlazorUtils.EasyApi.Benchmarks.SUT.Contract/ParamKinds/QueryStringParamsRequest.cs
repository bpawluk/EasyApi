using BlazorUtils.EasyApi.Benchmarks.SUT.Contract.Data;

namespace BlazorUtils.EasyApi.Benchmarks.SUT.Contract.ParamKinds;

[Route("param-kind/query-string")]
public class QueryStringParamsRequest : IPost
{
    [QueryStringParam]
    public int Integer { get; init; }

    [QueryStringParam]
    public float? NullableFloat { get; init; }

    [QueryStringParam]
    public char Character { get; init; }

    [QueryStringParam]
    public string String { get; init; } = null!;

    [QueryStringParam]
    public Gender Enum { get; init; }

    [QueryStringParam]
    public Person Class { get; init; } = null!;
}
