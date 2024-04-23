using BlazorUtils.EasyApi.Benchmarks.SUT.Contract.ParamTypes;
using BlazorUtils.EasyApi.Server;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorUtils.EasyApi.Benchmarks.SUT.Server.Handlers.ParamTypes;

public class StructParamsRequestHandler : IHandle<StructParamsRequest>
{
    public Task<HttpResult> Handle(StructParamsRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult.Ok());
}
