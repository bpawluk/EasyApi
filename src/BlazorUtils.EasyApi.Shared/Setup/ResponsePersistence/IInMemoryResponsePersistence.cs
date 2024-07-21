namespace BlazorUtils.EasyApi.Shared.Setup;

public readonly struct InMemoryResponsePersistenceOptions
{
    public bool IsEnabled { get; init; }
}

public interface IInMemoryResponsePersistence : IResponsePersistence
{
    InMemoryResponsePersistenceOptions Configure(IRequest request);
}
