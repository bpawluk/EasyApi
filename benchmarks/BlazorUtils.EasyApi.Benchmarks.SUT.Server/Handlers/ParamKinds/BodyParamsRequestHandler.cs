using BlazorUtils.EasyApi.Benchmarks.SUT.Contract.ParamKinds;
using BlazorUtils.EasyApi.Server;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorUtils.EasyApi.Benchmarks.SUT.Server.Handlers.ParamKinds;

public class BodyParamsRequestHandler : IHandle<BodyParamsRequest>
{
    public Task<HttpResult> Handle(BodyParamsRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult.Ok());
}
