using BlazorUtils.EasyApi.Tests.SUT.Contract.Data;
using BlazorUtils.EasyApi.Tests.SUT.Contract.Params;

namespace BlazorUtils.EasyApi.Tests.ParamTests;

public abstract class CollectionParamsTestsBase(TestsFixture fixture) : TestsBase(fixture)
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
            ArrayDefault = default,
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
            EnumerableDefault = default,
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
            ListDefault = default,
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

    [Fact]
    public async Task Request_WithCollectionParams_Set()
    {
        var request = new SetParamsRequest()
        {
            IntSet = new SortedSet<int>(_numbers),
            NullableIntSet = _nullableNumbers.ToHashSet(),
            StructSet = _prices.ToHashSet(),
            NullableStructSet = _nullablePrices.ToHashSet(),
            StringSet = new SortedSet<string>(_strings),
            ClassSet = _products.ToHashSet(),
            SetEmpty = new HashSet<object>(),
            SetDefault = default,
        };

        var result = await CallHttp<SetParamsRequest, SetParamsRequest.Response>(request);

        Assert.Equal(request.IntSet, result.IntSet);
        Assert.Equal(request.NullableIntSet, result.NullableIntSet!);
        Assert.Equal(request.StructSet, result.StructSet);
        Assert.Equal(request.NullableStructSet, result.NullableStructSet!);
        Assert.Equal(request.StringSet, result.StringSet);
        Assert.Equal(request.ClassSet, result.ClassSet);
        Assert.Equal(request.SetEmpty, result.SetEmpty);
        Assert.Equal(request.SetDefault, result.SetDefault!);
    }

    [Fact]
    public async Task Request_WithCollectionParams_Stack()
    {
        var request = new StackParamsRequest()
        {
            IntStack = new Stack<int>(_numbers),
            NullableIntStack = new Stack<int?>(_nullableNumbers),
            StructStack = new Stack<Price>(_prices),
            NullableStructStack = new Stack<Price?>(_nullablePrices),
            StringStack = new Stack<string>(_strings),
            ClassStack = new Stack<Product>(_products),
            StackEmpty = new(),
            StackDefault = default,
        };

        var result = await CallHttp<StackParamsRequest, StackParamsRequest.Response>(request);

        Assert.Equal(request.IntStack, result.IntStack);
        Assert.Equal(request.NullableIntStack, result.NullableIntStack!);
        Assert.Equal(request.StructStack, result.StructStack);
        Assert.Equal(request.NullableStructStack, result.NullableStructStack!);
        Assert.Equal(request.StringStack, result.StringStack);
        Assert.Equal(request.ClassStack, result.ClassStack);
        Assert.Equal(request.StackEmpty, result.StackEmpty);
        Assert.Equal(request.StackDefault, result.StackDefault!);
    }

    [Fact]
    public async Task Request_WithCollectionParams_Queue()
    {
        var request = new QueueParamsRequest()
        {
            IntQueue = new Queue<int>(_numbers),
            NullableIntQueue = new Queue<int?>(_nullableNumbers),
            StructQueue = new Queue<Price>(_prices),
            NullableStructQueue = new Queue<Price?>(_nullablePrices),
            StringQueue = new Queue<string>(_strings),
            ClassQueue = new Queue<Product>(_products),
            QueueEmpty = new(),
            QueueDefault = default,
        };

        var result = await CallHttp<QueueParamsRequest, QueueParamsRequest.Response>(request);

        Assert.Equal(request.IntQueue, result.IntQueue);
        Assert.Equal(request.NullableIntQueue, result.NullableIntQueue!);
        Assert.Equal(request.StructQueue, result.StructQueue);
        Assert.Equal(request.NullableStructQueue, result.NullableStructQueue!);
        Assert.Equal(request.StringQueue, result.StringQueue);
        Assert.Equal(request.ClassQueue, result.ClassQueue);
        Assert.Equal(request.QueueEmpty, result.QueueEmpty);
        Assert.Equal(request.QueueDefault, result.QueueDefault!);
    }

    [Fact]
    public async Task Request_WithCollectionParams_Dictionary()
    {
        var request = new DictionaryParamsRequest()
        {
            StringIntDictionary = new Dictionary<string, int>() { ["one"] = 1, ["two"] = 2, ["three"] = 3 },
            IntStringDictionary = new Dictionary<int, string>() { [1] = "one", [2] = "two", [3] = "three" },
            StringNullableIntDictionary = new Dictionary<string, int?>() { ["one"] = 1, ["null-1"] = null, ["two"] = 2, ["null-2"] = null, ["three"] = 3 },
            StringStructDictionary = new Dictionary<string, Price>() { ["one"] = new(), ["two"] = new() { Amount = 9.99M, Currency = "USD" } },
            StringNullableStructDictionary = new Dictionary<string, Price?>() { ["one"] = new(), ["two"] = null, ["three"] = new() { Amount = 9.99M, Currency = "USD" } },
            StringDictionary = new Dictionary<string, string>() { ["one"] = "eno", ["two"] = "owt", ["three"] = "eerht" },
            StringClassDictionary = new Dictionary<string, Product>() { ["one"] = new(), ["two"] = null!, ["three"] = new() { Name = "T-Shirt", Price = 9.99, StockQuantity = 100, CreatedAt = DateTime.UtcNow } },
            DictionaryEmpty = [],
            DictionaryDefault = default,
        };

        var result = await CallHttp<DictionaryParamsRequest, DictionaryParamsRequest.Response>(request);

        Assert.Equal(request.StringIntDictionary, result.StringIntDictionary);
        Assert.Equal(request.IntStringDictionary, result.IntStringDictionary);
        Assert.Equal(request.StringNullableIntDictionary, result.StringNullableIntDictionary);
        Assert.Equal(request.StringStructDictionary, result.StringStructDictionary);
        Assert.Equal(request.StringNullableStructDictionary, result.StringNullableStructDictionary);
        Assert.Equal(request.StringDictionary, result.StringDictionary);
        Assert.Equal(request.StringClassDictionary, result.StringClassDictionary);
        Assert.Equal(request.DictionaryEmpty, result.DictionaryEmpty);
        Assert.Equal(request.DictionaryDefault, result.DictionaryDefault);
    }
}
