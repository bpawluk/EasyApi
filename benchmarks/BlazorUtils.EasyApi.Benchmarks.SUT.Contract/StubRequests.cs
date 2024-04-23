using BlazorUtils.EasyApi.Benchmarks.SUT.Contract.Data;

namespace BlazorUtils.EasyApi.Benchmarks.SUT.Contract;

public class MethodRequestResponse<T>
{
    public T Value { get; init; } = default!;
}

[Route("stub")]
public class GetRequest : IGet
{
    [HeaderParam]
    public char Value { get; init; }
}

[Route("stub/with-response")]
public class GetRequestWithResponse : IGet<MethodRequestResponse<Guid>>
{
    [HeaderParam]
    public Guid Value { get; init; }
}

[Route($"stub")]
public class HeadRequest : IHead
{
    [HeaderParam]
    public string Value { get; init; } = default!;
}

[Route("stub")]
public class PatchRequest : IPatch
{
    [HeaderParam]
    public Uri Value { get; init; } = default!;
}

[Route("stub/with-response")]
public class PatchRequestWithResponse : IPatch<MethodRequestResponse<DateOnly>>
{
    [HeaderParam]
    public DateOnly Value { get; init; }
}

[Route("stub")]
public class PostRequest : IPost
{
    [HeaderParam]
    public TimeOnly Value { get; init; }
}

[Route("stub/with-response")]
public class PostRequestWithResponse : IPost<MethodRequestResponse<DateTime>>
{
    [HeaderParam]
    public DateTime Value { get; init; }
}

[Route("stub")]
public class PutRequest : IPut
{
    [HeaderParam]
    public int Value { get; init; }
}

[Route("stub/with-response")]
public class PutRequestWithResponse : IPut<MethodRequestResponse<int?>>
{
    [HeaderParam]
    public int? Value { get; init; }
}

[Route("stub")]
public class DeleteRequest : IDelete
{
    [HeaderParam]
    public Person Value { get; init; } = null!;
}

[Route("stub/with-response")]
public class DeleteRequestWithResponse : IDelete<MethodRequestResponse<Person[]>>
{
    [HeaderParam]
    public Person[] Value { get; init; } = null!;
}
