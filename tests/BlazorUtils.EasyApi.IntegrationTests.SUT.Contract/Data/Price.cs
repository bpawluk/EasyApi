namespace BlazorUtils.EasyApi.IntegrationTests.SUT.Contract.Data;

public readonly struct Price
{
    public decimal Amount { get; init; }

    public string Currency { get; init; }
}
