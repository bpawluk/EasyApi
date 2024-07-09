using System.Threading;
using System.Threading.Tasks;

namespace BlazorUtils.EasyApi.Shared.Persistence;

internal class PersistentCaller<Request, Response>(
    ICall<Request, Response> caller,
    IResponseStoreFactory storeFactory) 
    : IPersistentCall<Request, Response>
    where Request : class, IRequest<Response>, new()
{
    private readonly ICall<Request, Response> _caller = caller;
    private readonly IResponseStoreFactory _storeFactory = storeFactory;

    public Task<Response> Call(string storageKey, Request request) => Call(storageKey, request, CancellationToken.None);

    public async Task<Response> Call(string storageKey, Request request, CancellationToken cancellationToken)
    {
        var result = await CallHttp(storageKey, request, cancellationToken).ConfigureAwait(false);
        result.EnsureSucceeded();
        return result.Response!;
    }

    public Task<HttpResult<Response>> CallHttp(string storageKey, Request request) => CallHttp(storageKey, request, CancellationToken.None);

    public async Task<HttpResult<Response>> CallHttp(string storageKey, Request request, CancellationToken cancellationToken)
    {
        var responseStore = _storeFactory.GetStore(request);

        if (responseStore?.Retrieve(storageKey) is HttpResult<Response> persistedResult)
        {
            return persistedResult;
        }

        var result = await _caller.CallHttp(request, cancellationToken).ConfigureAwait(false);

        if (result.IsSuccessStatusCode && result.HasResponse)
        {
            responseStore?.Save(storageKey, result);
        }

        return result;
    }
}
