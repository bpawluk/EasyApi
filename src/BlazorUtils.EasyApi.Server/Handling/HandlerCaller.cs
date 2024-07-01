using System.Threading;
using System.Threading.Tasks;

namespace BlazorUtils.EasyApi.Server.Handling;

internal class HandlerCaller<Request>(IHandle<Request> handler) 
    : ICall<Request>
    where Request : class, IRequest, new()
{
    private readonly IHandle<Request> _handler = handler;

    public Task Call(Request request) => Call(request, CancellationToken.None);

    public async Task Call(Request request, CancellationToken cancellationToken)
    {
        var result = await CallHttp(request, cancellationToken).ConfigureAwait(false);
        result.EnsureSucceeded();
    }

    public Task<HttpResult> CallHttp(Request request) => CallHttp(request, CancellationToken.None);

    public Task<HttpResult> CallHttp(Request request, CancellationToken cancellationToken) => _handler.Handle(request, cancellationToken);
}
