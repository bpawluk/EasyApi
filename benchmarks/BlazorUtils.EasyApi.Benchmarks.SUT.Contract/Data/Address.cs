namespace BlazorUtils.EasyApi.Benchmarks.SUT.Contract.Data;

public readonly struct Address(string city, string postalCode, string street)
{
    public string City { get; } = city;

    public string PostalCode { get; } = postalCode;

    public string Street { get; } = street;
}
