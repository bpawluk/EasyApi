using BlazorUtils.EasyApi.Shared.Persistence.Response;
using BlazorUtils.EasyApi.Shared.Rendering;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace BlazorUtils.EasyApi.Shared.Persistence.InMemory;

internal class InMemoryResponseStore<ResponseType> : IResponseStore<ResponseType>
{
    private readonly IMemoryCache _memoryCache;
    private readonly MemoryCacheEntryOptions _options;

    private bool ShouldSave { get; }

    private bool ShouldRetrieve { get; }

    public InMemoryResponseStore(
        IMemoryCache memoryCache,
        IInteractivityDetector detector,
        TimeSpan? absoluteExpiration,
        TimeSpan? slidingExpiration)
    {
        _memoryCache = memoryCache;
        _options = new();

        ShouldSave = detector.IsInteractive;
        ShouldRetrieve = detector.IsInteractive;

        if (absoluteExpiration.HasValue)
        {
            _options.AbsoluteExpirationRelativeToNow = absoluteExpiration;
        }

        if (slidingExpiration.HasValue)
        {
            _options.SlidingExpiration = slidingExpiration;
        }
    }

    public void Save(string key, HttpResult<ResponseType> response)
    {
        if (ShouldSave)
        {
            var snapshot = new ResponseSnapshot<ResponseType>(response.StatusCode, response.Response!);
            _memoryCache.Set(key, snapshot, _options);
        }
    }

    public PersistedResponse<ResponseType>? Retrieve(string key)
    {
        if (ShouldRetrieve
            && _memoryCache.TryGetValue(key, out var result)
            && result is ResponseSnapshot<ResponseType> snapshot)
        {
            var response = HttpResult<ResponseType>.WithStatusCode(snapshot.StatusCode, snapshot.Response);
            return PersistedResponse<ResponseType>.Create(response);
        }
        return null;
    }
}
