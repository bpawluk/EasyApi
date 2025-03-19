namespace BlazorUtils.EasyApi.IntegrationTests.SUT.Contract;

[Route("get-user-name")]
public record GetUserNameRequest : IGet<GetUserNameRequest.Response>
{
    public record Response(string? Name);
}
