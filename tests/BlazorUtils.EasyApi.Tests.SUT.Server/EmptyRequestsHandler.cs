using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Tests.SUT.Contract;

namespace BlazorUtils.EasyApi.Tests.SUT.Server;

public class EmptyRequestsHandler : IHandle<EmptyGet>, IHandle<EmptyPost>
{
    public Task<HttpResult> Handle(EmptyGet request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult.Ok());

    public Task<HttpResult> Handle(EmptyPost request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult.Ok());
}
