using BlazorUtils.EasyApi.Shared.Persistence;
using Microsoft.AspNetCore.Components;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorUtils.EasyApi.Client.Persistence;

internal class PersistentCaller<Request, Response>(
    ICall<Request, Response> caller, 
    PersistentComponentState state) 
    : IPersistentCall<Request, Response>
    where Request : class, IRequest<Response>, new()
{
    private readonly ICall<Request, Response> _caller = caller;
    private readonly PersistentComponentState _state = state;

    public Task<Response> Call(string storageKey, Request request) 
        => Call(storageKey, request, CancellationToken.None);

    public async Task<Response> Call(string storageKey, Request request, CancellationToken cancellationToken)
    {
        var result = await CallHttp(storageKey, request, cancellationToken).ConfigureAwait(false);
        result.EnsureSucceeded();
        return result.Response!;
    }

    public Task<HttpResult<Response>> CallHttp(string storageKey, Request request) 
        => CallHttp(storageKey, request, CancellationToken.None);

    public async Task<HttpResult<Response>> CallHttp(string storageKey, Request request, CancellationToken cancellationToken)
    {
        if (GetPersistedResponse(storageKey) is HttpResult<Response> result)
        {
            return result;
        }
        return await _caller.CallHttp(request, cancellationToken).ConfigureAwait(false);
    }

    private HttpResult<Response>? GetPersistedResponse(string storageKey)
    {
        if (_state.TryTakeFromJson<ResponseSnapshot<Response>>(storageKey, out var result))
        {
            return HttpResult<Response>.WithStatusCode(result!.StatusCode, result.Response);
        }
        return null;
    }
}
