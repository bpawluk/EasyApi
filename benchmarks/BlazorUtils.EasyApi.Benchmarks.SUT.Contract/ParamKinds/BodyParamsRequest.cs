using BlazorUtils.EasyApi.Benchmarks.SUT.Contract.Data;

namespace BlazorUtils.EasyApi.Benchmarks.SUT.Contract.ParamKinds;

[Route("param-kind/body")]
public class BodyParamsRequest : IPost
{
    [BodyParam]
    public int Integer { get; init; }

    [BodyParam]
    public float? NullableFloat { get; init; }

    [BodyParam]
    public char Character { get; init; }

    [BodyParam]
    public string String { get; init; } = null!;

    [BodyParam]
    public Gender Enum { get; init; }

    [BodyParam]
    public Person Class { get; init; } = null!;
}
