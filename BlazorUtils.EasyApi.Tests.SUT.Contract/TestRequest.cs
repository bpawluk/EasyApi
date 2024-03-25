using static BlazorUtils.EasyApi.Tests.SUT.Contract.TestRequest;

namespace BlazorUtils.EasyApi.Tests.SUT.Contract;

[Route("test")]
public class TestRequest : IPost<Response>
{
    [BodyParam]
    public int Number { get; init; }

    public class Response 
    {
        public int Number { get; init; }
    }
}
