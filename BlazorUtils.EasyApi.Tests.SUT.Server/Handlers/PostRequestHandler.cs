using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Tests.SUT.Contract;

namespace BlazorUtils.EasyApi.Tests.SUT.Server.Handlers;

public class PostRequestHandler : IHandle<PostRequest>
{
    public Task Handle(PostRequest request, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
