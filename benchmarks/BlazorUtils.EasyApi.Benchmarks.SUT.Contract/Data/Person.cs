namespace BlazorUtils.EasyApi.Benchmarks.SUT.Contract.Data;

public class Person
{
    public string Name { get; init; } = null!;

    public int Age { get; init; }

    public DateOnly DateOfBirth { get; init; }

    public Gender Gender { get; init; }

    public Address Address { get; init; }
}
