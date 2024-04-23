using BlazorUtils.EasyApi.Benchmarks.SUT.Contract.ParamKinds;
using BlazorUtils.EasyApi.Server;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorUtils.EasyApi.Benchmarks.SUT.Server.Handlers.ParamKinds;

public class RouteParamsRequestHandler : IHandle<RouteParamsRequest>
{
    public Task<HttpResult> Handle(RouteParamsRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult.Ok());
}
