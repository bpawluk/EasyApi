using System.Threading;
using System.Threading.Tasks;

namespace BlazorUtils.EasyApi;

public interface IPersistentCall<Request, Response> : ICall
    where Request : class, IRequest<Response>, new()
{
    Task<Response> Call(string storageKey, Request request, CancellationToken cancellationToken);

    Task<Response> Call(
        string storageKey,
        Request request,
        bool forceFreshCall = false,
        CancellationToken cancellationToken = default);

    Task<HttpResult<Response>> CallHttp(string storageKey, Request request, CancellationToken cancellationToken);

    Task<HttpResult<Response>> CallHttp(
        string storageKey,
        Request request,
        bool forceFreshCall = false,
        CancellationToken cancellationToken = default);
}
