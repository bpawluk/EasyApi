using BlazorUtils.EasyApi.Tests.SUT.Contract.Data;
using BlazorUtils.EasyApi.Tests.SUT.Contract.ParamTypes;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorUtils.EasyApi.Tests.ParamTypes;

public abstract class CollectionParamsTests(TestsFixture fixture) : TestsBase(fixture)
{
    private readonly IEnumerable<int> _numbers = [1, 2, 3];
    private readonly IEnumerable<int?> _nullableNumbers = [1, null, 2, null, 3];
    private readonly IEnumerable<Price> _prices = [new(), new() { Amount = 9.99M, Currency = "USD" }];
    private readonly IEnumerable<Price?> _nullablePrices = [new(), null, new() { Amount = 9.99M, Currency = "USD" }];
    private readonly IEnumerable<string> _strings = ["string", string.Empty, null!];
    private readonly IEnumerable<Product> _products = [new(), null!, new() { Name = "T-Shirt", Price = 9.99, StockQuantity = 100, CreatedAt = DateTime.UtcNow }];

    [Fact]
    public async Task Request_WithCollectionParams_Array()
    {
        var request = new ArrayParamsRequest()
        {
            IntArray = _numbers.ToArray(),
            NullableIntArray = _nullableNumbers.ToArray(),
            StructArray = _prices.ToArray(),
            NullableStructArray = _nullablePrices.ToArray(),
            StringArray = _strings.ToArray(),
            ClassArray = _products.ToArray(),
            ArrayEmpty = [],
            ArrayDefault = default!,
        };

        var result = await CallHttp<ArrayParamsRequest, ArrayParamsRequest.Response>(request);

        Assert.Equal(request.IntArray, result.IntArray);
        Assert.Equal(request.NullableIntArray, result.NullableIntArray);
        Assert.Equal(request.StructArray, result.StructArray);
        Assert.Equal(request.NullableStructArray, result.NullableStructArray);
        Assert.Equal(request.StringArray, result.StringArray);
        Assert.Equal(request.ClassArray, result.ClassArray);
        Assert.Equal(request.ArrayEmpty, result.ArrayEmpty);
        Assert.Equal(request.ArrayDefault, result.ArrayDefault);
    }

    [Fact]
    public async Task Request_WithCollectionParams_Enumerable()
    {
        var request = new EnumerableParamsRequest()
        {
            IntEnumerable = _numbers,
            NullableIntEnumerable = _nullableNumbers,
            StructEnumerable = _prices,
            NullableStructEnumerable = _nullablePrices,
            StringEnumerable = _strings,
            ClassEnumerable = _products,
            EnumerableEmpty = [],
            EnumerableDefault = default!,
        };

        var result = await CallHttp<EnumerableParamsRequest, EnumerableParamsRequest.Response>(request);

        Assert.Equal(request.IntEnumerable, result.IntEnumerable);
        Assert.Equal(request.NullableIntEnumerable, result.NullableIntEnumerable!);
        Assert.Equal(request.StructEnumerable, result.StructEnumerable);
        Assert.Equal(request.NullableStructEnumerable, result.NullableStructEnumerable!);
        Assert.Equal(request.StringEnumerable, result.StringEnumerable);
        Assert.Equal(request.ClassEnumerable, result.ClassEnumerable);
        Assert.Equal(request.EnumerableEmpty, result.EnumerableEmpty);
        Assert.Equal(request.EnumerableDefault, result.EnumerableDefault!);
    }

    [Fact]
    public async Task Request_WithCollectionParams_List()
    {
        var request = new ListParamsRequest()
        {
            IntList = _numbers.ToList(),
            NullableIntList = _nullableNumbers.ToList(),
            StructList = _prices.ToList(),
            NullableStructList = _nullablePrices.ToList(),
            StringList = _strings.ToList(),
            ClassList = _products.ToList(),
            ListEmpty = [],
            ListDefault = default!,
        };

        var result = await CallHttp<ListParamsRequest, ListParamsRequest.Response>(request);

        Assert.Equal(request.IntList, result.IntList);
        Assert.Equal(request.NullableIntList, result.NullableIntList!);
        Assert.Equal(request.StructList, result.StructList);
        Assert.Equal(request.NullableStructList, result.NullableStructList!);
        Assert.Equal(request.StringList, result.StringList);
        Assert.Equal(request.ClassList, result.ClassList);
        Assert.Equal(request.ListEmpty, result.ListEmpty);
        Assert.Equal(request.ListDefault, result.ListDefault!);
    }

    // hash set

    // stack

    // queue

    // dictionary
}

public class Client_CollectionParamsTests(TestsFixture fixture) : CollectionParamsTests(fixture)
{
    protected override ICall<Request> GetCaller<Request>() => _client.Services.GetRequiredService<ICall<Request>>();

    protected override ICall<Request, Response> GetCaller<Request, Response>() => _client.Services.GetRequiredService<ICall<Request, Response>>();
}

public class Server_CollectionParamsTests(TestsFixture fixture) : CollectionParamsTests(fixture)
{
    protected override ICall<Request> GetCaller<Request>() => _server.Services.GetRequiredService<ICall<Request>>();

    protected override ICall<Request, Response> GetCaller<Request, Response>() => _server.Services.GetRequiredService<ICall<Request, Response>>();
}
