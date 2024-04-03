using BlazorUtils.EasyApi.Tests.SUT.Contract.Data;

namespace BlazorUtils.EasyApi.Tests.SUT.Contract.ParamTypes;

[Route("param-type/collection/array")]
public class ArrayParamsRequest : IPost<ArrayParamsRequest.Response>
{
    [HeaderParam] public int[] IntArray { get; init; } = null!;
    [QueryStringParam] public int?[]? NullableIntArray { get; init; }

    [BodyParam] public Price[] StructArray { get; init; } = null!;
    [HeaderParam] public Price?[]? NullableStructArray { get; init; }

    [QueryStringParam] public string[] StringArray { get; init; } = null!;
    [BodyParam] public Product[] ClassArray { get; init; } = null!;

    [HeaderParam] public object[] ArrayEmpty { get; init; } = null!;
    [QueryStringParam] public object[]? ArrayDefault { get; init; } 

    public class Response
    {
        public int[] IntArray { get; init; } = null!;
        public int?[]? NullableIntArray { get; init; }

        public Price[] StructArray { get; init; } = null!;
        public Price?[]? NullableStructArray { get; init; }

        public string[] StringArray { get; init; } = null!;
        public Product[] ClassArray { get; init; } = null!;

        public object[] ArrayEmpty { get; init; } = null!;
        public object[]? ArrayDefault { get; init; }
    }
}

[Route("param-type/collection/enumerable")]
public class EnumerableParamsRequest : IPost<EnumerableParamsRequest.Response>
{
    [HeaderParam] public IEnumerable<int> IntEnumerable { get; init; } = null!;
    [QueryStringParam] public IEnumerable<int?>? NullableIntEnumerable { get; init; }

    [BodyParam] public IEnumerable<Price> StructEnumerable { get; init; } = null!;
    [HeaderParam] public IEnumerable<Price?>? NullableStructEnumerable { get; init; }

    [QueryStringParam] public IEnumerable<string> StringEnumerable { get; init; } = null!;
    [BodyParam] public IEnumerable<Product> ClassEnumerable { get; init; } = null!;

    [HeaderParam] public IEnumerable<object> EnumerableEmpty { get; init; } = null!;
    [QueryStringParam] public IEnumerable<object>? EnumerableDefault { get; init; }

    public class Response
    {
        public IEnumerable<int> IntEnumerable { get; init; } = null!;
        public IEnumerable<int?>? NullableIntEnumerable { get; init; }

        public IEnumerable<Price> StructEnumerable { get; init; } = null!;
        public IEnumerable<Price?>? NullableStructEnumerable { get; init; }

        public IEnumerable<string> StringEnumerable { get; init; } = null!;
        public IEnumerable<Product> ClassEnumerable { get; init; } = null!;

        public IEnumerable<object> EnumerableEmpty { get; init; } = null!;
        public IEnumerable<object>? EnumerableDefault { get; init; }
    }
}

[Route("param-type/collection/list")]
public class ListParamsRequest : IPost<ListParamsRequest.Response>
{
    [HeaderParam] public List<int> IntList { get; init; } = null!;
    [QueryStringParam] public IList<int?>? NullableIntList { get; init; }

    [BodyParam] public IList<Price> StructList { get; init; } = null!;
    [HeaderParam] public List<Price?>? NullableStructList { get; init; }

    [QueryStringParam] public List<string> StringList { get; init; } = null!;
    [BodyParam] public IList<Product> ClassList { get; init; } = null!;

    [HeaderParam] public IList<object> ListEmpty { get; init; } = null!;
    [QueryStringParam] public List<object>? ListDefault { get; init; }

    public class Response
    {
        public List<int> IntList { get; init; } = null!;
        public IList<int?>? NullableIntList { get; init; }

        public IList<Price> StructList { get; init; } = null!;
        public List<Price?>? NullableStructList { get; init; }

        public List<string> StringList { get; init; } = null!;
        public IList<Product> ClassList { get; init; } = null!;

        public IList<object> ListEmpty { get; init; } = null!;
        public List<object>? ListDefault { get; init; }
    }
}
