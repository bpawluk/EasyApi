using Microsoft.Extensions.Caching.Memory;
using System;

namespace BlazorUtils.EasyApi.Shared.Persistence.InMemory;

internal sealed class MemoryCacheProvider : IDisposable
{
    public IMemoryCache Cache { get; } = new MemoryCache(new MemoryCacheOptions());

    public void Dispose()
    {
        Cache.Dispose();
    }
}
