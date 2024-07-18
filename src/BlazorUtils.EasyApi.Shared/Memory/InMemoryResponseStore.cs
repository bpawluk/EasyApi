using BlazorUtils.EasyApi.Shared.Persistence;
using BlazorUtils.EasyApi.Shared.Rendering;
using System.Collections.Generic;

namespace BlazorUtils.EasyApi.Shared.Memory;

internal class InMemoryResponseStore<ResponseType>(IInteractivityDetector detector) : IResponseStore<ResponseType>
{
    private readonly Dictionary<string, ResponseSnapshot<ResponseType>> _persistedResponses = [];

    private bool ShouldPersist { get; } = detector.IsInteractive;

    public void Save(string key, HttpResult<ResponseType> response)
    {
        if (ShouldPersist)
        {
            _persistedResponses[key] = new(response.StatusCode, response.Response!);
        }
    }

    public HttpResult<ResponseType>? Retrieve(string key)
    {
        if (ShouldPersist && _persistedResponses.TryGetValue(key, out var result) && result is not null)
        {
            return HttpResult<ResponseType>.WithStatusCode(result.StatusCode, result.Response);
        }
        return null;
    }
}
