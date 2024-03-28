using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Tests.SUT.Contract;

namespace BlazorUtils.EasyApi.Tests.SUT.Server.Handlers;

public class PostRequestHandler : IHandle<PostRequest>
{
    Task<HttpResult> IHandle<PostRequest>.Handle(PostRequest request, CancellationToken cancellationToken) => Task.FromResult(HttpResult.Ok());
}
