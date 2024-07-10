using BlazorUtils.EasyApi.IntegrationTests.SUT.Contract.Data;

namespace BlazorUtils.EasyApi.IntegrationTests.SUT.Contract.Response;

[Route("response-type/collection/array")]
public class ArrayResponseRequest : IGet<int[]> 
{
    [HeaderParam] public int[] ExpectedResponse { get; init; } = null!;
}

[Route("response-type/collection/enumerable")]
public class EnumerableResponseRequest : IGet<IEnumerable<Price>>
{
    [HeaderParam] public IEnumerable<Price> ExpectedResponse { get; init; } = null!;
}

[Route("response-type/collection/list")]
public class ListResponseRequest : IGet<IList<Product>>
{
    [HeaderParam] public IList<Product> ExpectedResponse { get; init; } = null!;
}

[Route("response-type/collection/set")]
public class SetResponseRequest : IGet<ISet<float>>
{
    [HeaderParam] public ISet<float> ExpectedResponse { get; init; } = null!;
}

[Route("response-type/collection/stack")]
public class StackResponseRequest : IGet<Stack<string>>
{
    [HeaderParam] public Stack<string> ExpectedResponse { get; init; } = null!;
}

[Route("response-type/collection/queue")]
public class QueueResponseRequest : IGet<Queue<Price>>
{
    [HeaderParam] public Queue<Price> ExpectedResponse { get; init; } = null!;
}

[Route("response-type/collection/dictionary")]
public class DictionaryResponseRequest : IGet<IDictionary<string, Product>>
{
    [HeaderParam] public IDictionary<string, Product> ExpectedResponse { get; init; } = null!;
}
