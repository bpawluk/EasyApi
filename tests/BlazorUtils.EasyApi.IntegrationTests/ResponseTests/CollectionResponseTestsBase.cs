using BlazorUtils.EasyApi.IntegrationTests.SUT.Contract.Data;
using BlazorUtils.EasyApi.IntegrationTests.SUT.Contract.Response;
using System.Net;

namespace BlazorUtils.EasyApi.IntegrationTests.ResponseTests;

public abstract class CollectionResponseTestsBase(TestsFixture fixture) : TestsBase(fixture)
{
    public static TheoryData<int[]> ArrayValues => new()
    {
        Array.Empty<int>(),
        new int[] { 1, 2, 3 }
    };

    public static TheoryData<IEnumerable<Price>> EnumerableValues => new()
    {
        Array.Empty<Price>(),
        new Price[] { new(), new() { Amount = 9.99M, Currency = "USD" } },
        new List<Price>() { new() { Amount = 9.99M, Currency = "USD" } }
    };

    public static TheoryData<IList<Product>> ListValues => new()
    {
        new List<Product>(),
        new List<Product>() { new(), null!, new() { Name = "T-Shirt", Price = 9.99, StockQuantity = 100, CreatedAt = DateTime.UtcNow } }
    };

    public static TheoryData<ISet<float>> SetValues => new()
    {
        new HashSet<float>(),
        new HashSet<float>() { 1f, 2f, 3f },
        new SortedSet<float>() { 1f, 2f, 3f }
    };

    public static TheoryData<Stack<string>> StackValues => new()
    {
        new Stack<string>(),
        new Stack<string>(["1", "2", "3"])
    };

    public static TheoryData<Queue<Price>> QueueValues => new()
    {
        new Queue<Price>(),
        new Queue<Price>([new()])
    };

    public static TheoryData<IDictionary<string, Product>> DictionaryValues => new()
    {
        new Dictionary<string, Product>(),
        new Dictionary<string, Product>()
        {
            ["empty"] = new(),
            ["filled"] = new() { Name = "T-Shirt", Price = 9.99, StockQuantity = 100, CreatedAt = DateTime.UtcNow },
        }
    };

    [Theory]
    [MemberData(nameof(ArrayValues))]
    public async Task Response_OfArrayType_WithValue(int[] expectedResponse)
    {
        var request = new ArrayResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<ArrayResponseRequest, int[]>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Fact]
    public async Task Response_OfArrayType_NoValue()
    {
        var request = new ArrayResponseRequest() { ExpectedResponse = null! };
        var caller = GetCaller<ArrayResponseRequest, int[]>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }

    [Theory]
    [MemberData(nameof(EnumerableValues))]
    public async Task Response_OfEnumerableType_WithValue(IEnumerable<Price> expectedResponse)
    {
        var request = new EnumerableResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<EnumerableResponseRequest, IEnumerable<Price>>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Fact]
    public async Task Response_OfEnumerableType_NoValue()
    {
        var request = new EnumerableResponseRequest() { ExpectedResponse = null! };
        var caller = GetCaller<EnumerableResponseRequest, IEnumerable<Price>>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }

    [Theory]
    [MemberData(nameof(ListValues))]
    public async Task Response_OfListType_WithValue(IList<Product> expectedResponse)
    {
        var request = new ListResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<ListResponseRequest, IList<Product>>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Fact]
    public async Task Response_OfListType_NoValue()
    {
        var request = new ListResponseRequest() { ExpectedResponse = null! };
        var caller = GetCaller<ListResponseRequest, IList<Product>>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }

    [Theory]
    [MemberData(nameof(SetValues))]
    public async Task Response_OfSetType_WithValue(ISet<float> expectedResponse)
    {
        var request = new SetResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<SetResponseRequest, ISet<float>>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Fact]
    public async Task Response_OfSetType_NoValue()
    {
        var request = new SetResponseRequest() { ExpectedResponse = null! };
        var caller = GetCaller<SetResponseRequest, ISet<float>>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }

    [Theory]
    [MemberData(nameof(StackValues))]
    public async Task Response_OfStackType_WithValue(Stack<string> expectedResponse)
    {
        var request = new StackResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<StackResponseRequest, Stack<string>>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Fact]
    public async Task Response_OfStackType_NoValue()
    {
        var request = new StackResponseRequest() { ExpectedResponse = null! };
        var caller = GetCaller<StackResponseRequest, Stack<string>>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }

    [Theory]
    [MemberData(nameof(QueueValues))]
    public async Task Response_OfQueueType_WithValue(Queue<Price> expectedResponse)
    {
        var request = new QueueResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<QueueResponseRequest, Queue<Price>>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Fact]
    public async Task Response_OfQueueType_NoValue()
    {
        var request = new QueueResponseRequest() { ExpectedResponse = null! };
        var caller = GetCaller<QueueResponseRequest, Queue<Price>>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }

    [Theory]
    [MemberData(nameof(DictionaryValues))]
    public async Task Response_OfDictionaryType_WithValue(IDictionary<string, Product> expectedResponse)
    {
        var request = new DictionaryResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<DictionaryResponseRequest, IDictionary<string, Product>>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Fact]
    public async Task Response_OfDictionaryType_NoValue()
    {
        var request = new DictionaryResponseRequest() { ExpectedResponse = null! };
        var caller = GetCaller<DictionaryResponseRequest, IDictionary<string, Product>>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }
}
