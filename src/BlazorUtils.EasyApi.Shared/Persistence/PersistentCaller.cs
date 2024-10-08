﻿using BlazorUtils.EasyApi.Shared.Persistence.Response;
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

    public Task<Response> Call(string storageKey, Request request, CancellationToken cancellationToken)
        => Call(storageKey, request, false, cancellationToken);

    public async Task<Response> Call(
        string storageKey, 
        Request request,
        bool forceFreshCall,
        CancellationToken cancellationToken)
    {
        var result = await CallHttp(storageKey, request, forceFreshCall, cancellationToken).ConfigureAwait(false);
        result.EnsureSucceeded();
        return result.Response!;
    }

    public Task<HttpResult<Response>> CallHttp(string storageKey, Request request, CancellationToken cancellationToken)
        => CallHttp(storageKey, request, false, cancellationToken);

    public async Task<HttpResult<Response>> CallHttp(
        string storageKey, 
        Request request, 
        bool forceFreshCall, 
        CancellationToken cancellationToken)
    {
        var responseStore = _storeFactory.GetStore(request);

        if (!forceFreshCall && GetPersistedResponse(responseStore, storageKey, request) is HttpResult<Response> persistedResponse)
        {
            return persistedResponse;
        }

        var result = await _caller.CallHttp(request, cancellationToken).ConfigureAwait(false);

        if (result.IsSuccessStatusCode && result.HasResponse)
        {
            responseStore?.Save(storageKey, result);
        }

        return result;
    }

    public static HttpResult<Response>? GetPersistedResponse(IResponseStore<Response>? responseStore, string storageKey, Request request)
    {
        if (responseStore?.Retrieve(storageKey) is PersistedResponse<Response> persistedResponse)
        {
            if (persistedResponse.IsSticky)
            {
                responseStore?.Save(storageKey, persistedResponse.Value);
            }
            return persistedResponse.Value;
        }
        return null;
    }
}
