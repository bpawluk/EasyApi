using System.Net;

namespace BlazorUtils.EasyApi.IntegrationTests.SUT.Contract.Response;

[Route($"response-kind")]
public class NoResponseRequest : IHead
{
    [HeaderParam]
    public Guid Id { get; init; }

    [HeaderParam]
    public HttpStatusCode ExpectedStatusCode { get; init; }
}

[Route($"response-kind")]
public class WithResponseRequest : IGet<WithResponseRequest.Response>
{
    [HeaderParam]
    public Guid Id { get; init; }

    [HeaderParam]
    public HttpStatusCode ExpectedStatusCode { get; init; }

    [HeaderParam]
    public bool IncludeResponse { get; init; }

    public class Response
    {
        public Guid Id { get; init; }
    }
}
