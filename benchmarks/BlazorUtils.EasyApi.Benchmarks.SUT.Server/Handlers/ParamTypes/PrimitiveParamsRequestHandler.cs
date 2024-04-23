using BlazorUtils.EasyApi.Benchmarks.SUT.Contract.ParamTypes;
using BlazorUtils.EasyApi.Server;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorUtils.EasyApi.Benchmarks.SUT.Server.Handlers.ParamTypes;

public class PrimitiveParamsRequestHandler : IHandle<PrimitiveParamsRequest>
{
    public Task<HttpResult> Handle(PrimitiveParamsRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult.Ok());
}
