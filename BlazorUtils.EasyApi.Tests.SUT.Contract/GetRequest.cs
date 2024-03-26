using static BlazorUtils.EasyApi.Tests.SUT.Contract.GetRequest;

namespace BlazorUtils.EasyApi.Tests.SUT.Contract;

[Route("test")]
public class GetRequest : IGet<Response>
{
    [HeaderParam]
    public int Number { get; init; }

    public class Response 
    {
        public int Number { get; init; }
    }
}
