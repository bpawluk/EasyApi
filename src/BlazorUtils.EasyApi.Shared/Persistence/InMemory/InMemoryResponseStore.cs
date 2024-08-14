using BlazorUtils.EasyApi.Shared.Persistence.Response;
using BlazorUtils.EasyApi.Shared.Rendering;
using System.Collections.Generic;

namespace BlazorUtils.EasyApi.Shared.Persistence.InMemory;

internal class InMemoryResponseStore<ResponseType>(IInteractivityDetector detector) : IResponseStore<ResponseType>
{
    private readonly Dictionary<string, ResponseSnapshot<ResponseType>> _persistedResponses = [];

    private bool ShouldSave { get; } = detector.IsInteractive;

    private bool ShouldRetrieve { get; } = detector.IsInteractive;

    public void Save(string key, HttpResult<ResponseType> response)
    {
        if (ShouldSave)
        {
            _persistedResponses[key] = new(response.StatusCode, response.Response!);
        }
    }

    public PersistedResponse<ResponseType>? Retrieve(string key)
    {
        if (ShouldRetrieve && _persistedResponses.TryGetValue(key, out var result) && result is not null)
        {
            var response = HttpResult<ResponseType>.WithStatusCode(result.StatusCode, result.Response);
            return PersistedResponse<ResponseType>.Create(response);
        }
        return null;
    }
}
