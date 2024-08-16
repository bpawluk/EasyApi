using System;

namespace BlazorUtils.EasyApi.Shared.Setup;

public readonly struct InMemoryResponsePersistenceOptions
{
    public bool IsEnabled { get; init; }

    public TimeSpan? AbsoluteExpiration { get; init; }

    public TimeSpan? SlidingExpiration { get; init; }
}

public interface IInMemoryResponsePersistence : IResponsePersistence
{
    InMemoryResponsePersistenceOptions Configure(IRequest request);
}
