namespace BlazorUtils.EasyApi.Benchmarks.SUT.Contract.Data;

public class Product
{
    public int ProductId { get; init; }

    public string Name { get; init; } = null!;

    public decimal Price { get; init; }
}
