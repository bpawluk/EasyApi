using BlazorUtils.EasyApi.Tests.SUT.Contract.Data;

namespace BlazorUtils.EasyApi.Tests.SUT.Contract.Params;

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

[Route("param-type/collection/set")]
public class SetParamsRequest : IPost<SetParamsRequest.Response>
{
    [HeaderParam] public SortedSet<int> IntSet { get; init; } = null!;
    [QueryStringParam] public ISet<int?>? NullableIntSet { get; init; }

    [BodyParam] public ISet<Price> StructSet { get; init; } = null!;
    [HeaderParam] public HashSet<Price?>? NullableStructSet { get; init; }

    [QueryStringParam] public SortedSet<string> StringSet { get; init; } = null!;
    [BodyParam] public ISet<Product> ClassSet { get; init; } = null!;

    [HeaderParam] public ISet<object> SetEmpty { get; init; } = null!;
    [QueryStringParam] public HashSet<object>? SetDefault { get; init; }

    public class Response
    {
        public SortedSet<int> IntSet { get; init; } = null!;
        public ISet<int?>? NullableIntSet { get; init; }

        public ISet<Price> StructSet { get; init; } = null!;
        public HashSet<Price?>? NullableStructSet { get; init; }

        public SortedSet<string> StringSet { get; init; } = null!;
        public ISet<Product> ClassSet { get; init; } = null!;

        public ISet<object> SetEmpty { get; init; } = null!;
        public HashSet<object>? SetDefault { get; init; }
    }
}

[Route("param-type/collection/stack")]
public class StackParamsRequest : IPost<StackParamsRequest.Response>
{
    [HeaderParam] public Stack<int> IntStack { get; init; } = null!;
    [QueryStringParam] public Stack<int?>? NullableIntStack { get; init; }

    [BodyParam] public Stack<Price> StructStack { get; init; } = null!;
    [HeaderParam] public Stack<Price?>? NullableStructStack { get; init; }

    [QueryStringParam] public Stack<string> StringStack { get; init; } = null!;
    [BodyParam] public Stack<Product> ClassStack { get; init; } = null!;

    [HeaderParam] public Stack<object> StackEmpty { get; init; } = null!;
    [QueryStringParam] public Stack<object>? StackDefault { get; init; }

    public class Response
    {
        public Stack<int> IntStack { get; init; } = null!;
        public Stack<int?>? NullableIntStack { get; init; }

        public Stack<Price> StructStack { get; init; } = null!;
        public Stack<Price?>? NullableStructStack { get; init; }

        public Stack<string> StringStack { get; init; } = null!;
        public Stack<Product> ClassStack { get; init; } = null!;

        public Stack<object> StackEmpty { get; init; } = null!;
        public Stack<object>? StackDefault { get; init; }
    }
}

[Route("param-type/collection/queue")]
public class QueueParamsRequest : IPost<QueueParamsRequest.Response>
{
    [HeaderParam] public Queue<int> IntQueue { get; init; } = null!;
    [QueryStringParam] public Queue<int?>? NullableIntQueue { get; init; }

    [BodyParam] public Queue<Price> StructQueue { get; init; } = null!;
    [HeaderParam] public Queue<Price?>? NullableStructQueue { get; init; }

    [QueryStringParam] public Queue<string> StringQueue { get; init; } = null!;
    [BodyParam] public Queue<Product> ClassQueue { get; init; } = null!;

    [HeaderParam] public Queue<object> QueueEmpty { get; init; } = null!;
    [QueryStringParam] public Queue<object>? QueueDefault { get; init; }

    public class Response
    {
        public Queue<int> IntQueue { get; init; } = null!;
        public Queue<int?>? NullableIntQueue { get; init; }

        public Queue<Price> StructQueue { get; init; } = null!;
        public Queue<Price?>? NullableStructQueue { get; init; }

        public Queue<string> StringQueue { get; init; } = null!;
        public Queue<Product> ClassQueue { get; init; } = null!;

        public Queue<object> QueueEmpty { get; init; } = null!;
        public Queue<object>? QueueDefault { get; init; }
    }
}

[Route("param-type/collection/dictionary")]
public class DictionaryParamsRequest : IPost<DictionaryParamsRequest.Response>
{
    [HeaderParam] public Dictionary<string, int> StringIntDictionary { get; init; } = null!;
    [HeaderParam] public IDictionary<int, string> IntStringDictionary { get; init; } = null!;
    [QueryStringParam] public Dictionary<string, int?>? StringNullableIntDictionary { get; init; }

    [BodyParam] public IDictionary<string, Price> StringStructDictionary { get; init; } = null!;
    [HeaderParam] public Dictionary<string, Price?>? StringNullableStructDictionary { get; init; }

    [QueryStringParam] public Dictionary<string, string> StringDictionary { get; init; } = null!;
    [BodyParam] public Dictionary<string, Product> StringClassDictionary { get; init; } = null!;

    [HeaderParam] public Dictionary<object, object> DictionaryEmpty { get; init; } = null!;
    [QueryStringParam] public Dictionary<object, object>? DictionaryDefault { get; init; }

    public class Response
    {
        public Dictionary<string, int> StringIntDictionary { get; init; } = null!;
        public IDictionary<int, string> IntStringDictionary { get; init; } = null!;
        public Dictionary<string, int?>? StringNullableIntDictionary { get; init; }

        public IDictionary<string, Price> StringStructDictionary { get; init; } = null!;
        public Dictionary<string, Price?>? StringNullableStructDictionary { get; init; }

        public Dictionary<string, string> StringDictionary { get; init; } = null!;
        public Dictionary<string, Product> StringClassDictionary { get; init; } = null!;

        public Dictionary<object, object> DictionaryEmpty { get; init; } = null!;
        public Dictionary<object, object>? DictionaryDefault { get; init; }
    }
}
