using BlazorUtils.EasyApi.Benchmarks.SUT.Contract.Data;

namespace BlazorUtils.EasyApi.Benchmarks.SUT.Contract.ParamKinds;

[Route($"param-kind/route/" +
    $"{nameof(Integer)}/" +
    $"{nameof(NullableFloat)}/" +
    $"{nameof(Character)}/" +
    $"{nameof(String)}/" +
    $"{nameof(Enum)}/" +
    $"{nameof(Class)}")]
public class RouteParamsRequest : IPost
{
    [RouteParam]
    public int Integer { get; init; }

    [RouteParam]
    public float? NullableFloat { get; init; }

    [RouteParam]
    public char Character { get; init; }

    [RouteParam]
    public string String { get; init; } = null!;

    [RouteParam]
    public Gender Enum { get; init; }

    [RouteParam]
    public Person Class { get; init; } = null!;
}
