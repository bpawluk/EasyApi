using System.Net;
using static BlazorUtils.EasyApi.Tests.SUT.Contract.WithResponseRequest;

namespace BlazorUtils.EasyApi.Tests.SUT.Contract;

[Route($"response-kind")]
public class NoResponseRequest : IHead
{
    [HeaderParam]
    public Guid Id { get; init; }

    [HeaderParam]
    public HttpStatusCode ExpectedStatusCode { get; init; }
}

[Route($"response-kind")]
public class WithResponseRequest : IGet<Response>
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
