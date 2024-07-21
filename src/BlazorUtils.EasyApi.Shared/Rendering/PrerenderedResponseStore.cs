using BlazorUtils.EasyApi.Shared.Persistence;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorUtils.EasyApi.Shared.Rendering;

internal class PrerenderedResponseStore<ResponseType> : IResponseStore<ResponseType>, IDisposable
{
    private readonly PersistentComponentState _state;
    private readonly PersistingComponentStateSubscription? _subscription;
    private readonly Dictionary<string, ResponseSnapshot<ResponseType>> _responsesToPersist = [];

    private bool ShouldSave { get; }

    private bool ShouldRetrieve { get; }

    public PrerenderedResponseStore(PersistentComponentState state, IInteractivityDetector detector)
    {
        _state = state;

        ShouldSave = !detector.IsInteractive;
        ShouldRetrieve = detector.IsInteractive;

        if (ShouldSave)
        {
            _subscription = _state.RegisterOnPersisting(OnPersisting, RenderMode.InteractiveAuto);
        }
    }

    public void Save(string key, HttpResult<ResponseType> response)
    {
        if (ShouldSave)
        {
            _responsesToPersist[key] = new(response.StatusCode, response.Response!);
        }
    }

    public PersistedResponse<ResponseType>? Retrieve(string key)
    {
        if (ShouldRetrieve && _state.TryTakeFromJson<ResponseSnapshot<ResponseType>>(key, out var result) && result is not null)
        {
            var response = HttpResult<ResponseType>.WithStatusCode(result.StatusCode, result.Response);
            return PersistedResponse<ResponseType>.Sticky(response);
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
