using System.Threading;
using System.Threading.Tasks;

namespace BlazorUtils.EasyApi;

public interface ICall { }

public interface ICall<Request> : ICall
    where Request : class, IRequest, new()
{
    Task Call(Request request);

    Task Call(Request request, CancellationToken cancellationToken);

    Task<HttpResult> CallHttp(Request request);

    Task<HttpResult> CallHttp(Request request, CancellationToken cancellationToken);
}

public interface ICall<Request, Response> : ICall
    where Request : class, IRequest<Response>, new()
{
    Task<Response> Call(Request request);

    Task<Response> Call(Request request, CancellationToken cancellationToken);

    Task<HttpResult<Response>> CallHttp(Request request);

    Task<HttpResult<Response>> CallHttp(Request request, CancellationToken cancellationToken);
}
