using System.Threading;
using System.Threading.Tasks;

namespace BlazorUtils.EasyApi.Server;

public interface IHandle { }

public interface IHandle<Request> : IHandle
    where Request : class, IRequest, new()
{
    Task<HttpResult> Handle(Request request, CancellationToken cancellationToken);
}

public interface IHandle<Request, Response> : IHandle
    where Request : class, IRequest<Response>, new()
{
    Task<HttpResult<Response>> Handle(Request request, CancellationToken cancellationToken);
}
