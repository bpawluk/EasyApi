using BlazorUtils.EasyApi.Benchmarks.SUT.Contract.Data;

namespace BlazorUtils.EasyApi.Benchmarks.SUT.Contract.ParamKinds;

[Route("param-kind/header")]
public class HeaderParamsRequest : IPost
{
    [HeaderParam]
    public int Integer { get; init; }

    [HeaderParam]
    public float? NullableFloat { get; init; }

    [HeaderParam]
    public char Character { get; init; }

    [HeaderParam]
    public string String { get; init; } = null!;

    [HeaderParam]
    public Gender Enum { get; init; }

    [HeaderParam]
    public Person Class { get; init; } = null!;
}
