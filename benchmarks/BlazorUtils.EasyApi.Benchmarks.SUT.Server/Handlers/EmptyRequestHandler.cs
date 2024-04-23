using BlazorUtils.EasyApi.Benchmarks.SUT.Contract;
using BlazorUtils.EasyApi.Server;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorUtils.EasyApi.Benchmarks.SUT.Server.Handlers;

public class EmptyRequestHandler : IHandle<EmptyRequest>
{
    public Task<HttpResult> Handle(EmptyRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult.Ok());
}
