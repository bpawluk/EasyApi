namespace BlazorUtils.EasyApi.Tests.SUT.Contract.Data;

public class Product
{
    public string Name { get; init; } = default!;

    public double Price { get; init; }

    public int StockQuantity { get; init; }

    public DateTime CreatedAt { get; init; }

    public override bool Equals(object? other)
    {
        if (other == null || GetType() != other.GetType())
        {
            return false;
        }
        var otherProduct = other as Product;
        return Name == otherProduct!.Name
            && Price == otherProduct.Price
            && StockQuantity == otherProduct.StockQuantity
            && CreatedAt == otherProduct.CreatedAt;
    }

    public override int GetHashCode() => base.GetHashCode();
}
