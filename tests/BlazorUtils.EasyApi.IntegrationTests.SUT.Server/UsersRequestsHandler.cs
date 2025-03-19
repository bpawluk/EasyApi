using BlazorUtils.EasyApi.IntegrationTests.SUT.Contract;
using BlazorUtils.EasyApi.Server;
using System.Security.Claims;

namespace BlazorUtils.EasyApi.IntegrationTests.SUT.Server;

public class UsersRequestsHandler : Handler<GetUserNameRequest, GetUserNameRequest.Response>
{
    public override async Task<HttpResult<GetUserNameRequest.Response>> Handle(GetUserNameRequest request, CancellationToken cancellationToken)
    {
        var user = await GetUser();
        var username = user.FindFirstValue("name");
        return HttpResult<GetUserNameRequest.Response>.Ok(new(username));
    }
}
