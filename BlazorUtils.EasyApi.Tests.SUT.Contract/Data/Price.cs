namespace BlazorUtils.EasyApi.Tests.SUT.Contract.Data;

public readonly struct Price
{
    public decimal Amount { get; init; }

    public string Currency { get; init; }
}
