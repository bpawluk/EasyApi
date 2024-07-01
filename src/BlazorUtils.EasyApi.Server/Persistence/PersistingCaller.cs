using BlazorUtils.EasyApi.Shared.Persistence;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorUtils.EasyApi.Server.Persistence;

internal class PersistingCaller<Request, Response> 
    : IPersistentCall<Request, Response>, IDisposable
    where Request : class, IRequest<Response>, new()
{
    private readonly ICall<Request, Response> _caller;

    private readonly PersistentComponentState _state;
    private readonly PersistingComponentStateSubscription? _subscription;

    private readonly Dictionary<string, ResponseSnapshot<Response>> _responsesToPersist = [];

    private bool ShouldPersist => _subscription is not null;

    public PersistingCaller(
        ICall<Request, Response> caller,
        PersistentComponentState state,
        PrerenderingDetector detector)
    {
        _caller = caller;
        _state = state;

        if (detector.IsPrerendering)
        {
            _subscription = _state.RegisterOnPersisting(OnPersisting, RenderMode.InteractiveAuto);
        }
    }

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
        if (GetPersistedResponse(storageKey) is HttpResult<Response> persistedResult)
        {
            return persistedResult;
        }

        var result = await _caller.CallHttp(request, cancellationToken).ConfigureAwait(false);

        if (ShouldPersist && result.HasResponse)
        {
            _responsesToPersist[storageKey] = new(result.StatusCode, result.Response!);
        }

        return result;
    }

    private HttpResult<Response>? GetPersistedResponse(string storageKey)
    {
        if (_state.TryTakeFromJson<ResponseSnapshot<Response>>(storageKey, out var result))
        {
            return HttpResult<Response>.WithStatusCode(result!.StatusCode, result.Response);
        }
        return null;
    }

    private Task OnPersisting()
    {
        foreach (var response in _responsesToPersist)
        {
            _state.PersistAsJson(response.Key, response.Value);
        }
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _subscription?.Dispose();
    }
}
