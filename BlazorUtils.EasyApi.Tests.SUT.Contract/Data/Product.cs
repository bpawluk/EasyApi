namespace BlazorUtils.EasyApi.Tests.SUT.Contract.Data;

public class Product
{
    public string Name { get; init; } = default!;

    public double Price { get; init; }

    public int StockQuantity { get; init; }

    public DateTime CreatedAt { get; init; }
}
