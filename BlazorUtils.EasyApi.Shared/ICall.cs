using System.Threading;
using System.Threading.Tasks;

namespace BlazorUtils.EasyApi;

public interface ICall { }

public interface ICall<Request> : ICall
    where Request : class, IRequest, new()
{
    Task Call(Request request, CancellationToken cancellationToken);
}

public interface ICall<Request, Response> : ICall
    where Request : class, IRequest<Response>, new()
{
    Task<Response> Call(Request request, CancellationToken cancellationToken);
}
