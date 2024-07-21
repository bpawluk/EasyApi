namespace BlazorUtils.EasyApi.IntegrationTests.SUT.Contract;

public class MethodRequestResponse
{
    public Guid Id { get; init; }
}

[Route("method")]
public class GetRequest : IGet
{
    [HeaderParam]
    public Guid Id { get; init; }
}

[Route("method-with-response")]
public class GetRequestWithResponse : IGet<MethodRequestResponse>
{
    [HeaderParam]
    public Guid Id { get; init; }
}

[Route($"method")]
public class HeadRequest : IHead
{
    [HeaderParam]
    public Guid Id { get; init; }
}

[Route("method")]
public class PatchRequest : IPatch
{
    [HeaderParam]
    public Guid Id { get; init; }
}

[Route("method-with-response")]
public class PatchRequestWithResponse : IPatch<MethodRequestResponse>
{
    [HeaderParam]
    public Guid Id { get; init; }
}

[Route("method")]
public class PostRequest : IPost
{
    [HeaderParam]
    public Guid Id { get; init; }
}

[Route("method-with-response")]
public class PostRequestWithResponse : IPost<MethodRequestResponse>
{
    [HeaderParam]
    public Guid Id { get; init; }
}

[Route("method")]
public class PutRequest : IPut
{
    [HeaderParam]
    public Guid Id { get; init; }
}

[Route("method-with-response")]
public class PutRequestWithResponse : IPut<MethodRequestResponse>
{
    [HeaderParam]
    public Guid Id { get; init; }
}

[Route("method")]
public class DeleteRequest : IDelete
{
    [HeaderParam]
    public Guid Id { get; init; }
}

[Route("method-with-response")]
public class DeleteRequestWithResponse : IDelete<MethodRequestResponse>
{
    [HeaderParam]
    public Guid Id { get; init; }
}