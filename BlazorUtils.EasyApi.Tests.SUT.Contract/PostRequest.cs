namespace BlazorUtils.EasyApi.Tests.SUT.Contract;

[Route("test")]
public class PostRequest : IPost
{ 
    [BodyParam]
    public int Number { get; init; }
}
