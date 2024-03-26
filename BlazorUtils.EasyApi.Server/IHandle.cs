using System.Threading;
using System.Threading.Tasks;

namespace BlazorUtils.EasyApi.Server;

// TODO: use response wrapper type with http status

public interface IHandle { }

public interface IHandle<Request> : IHandle
    where Request : class, IRequest, new()
{
    Task Handle(Request request, CancellationToken cancellationToken);
}

public interface IHandle<Request, Response> : IHandle
    where Request : class, IRequest<Response>, new()
{
    Task<Response> Handle(Request request, CancellationToken cancellationToken);
}
