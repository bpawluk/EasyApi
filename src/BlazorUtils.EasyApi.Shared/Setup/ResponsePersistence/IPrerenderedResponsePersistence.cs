namespace BlazorUtils.EasyApi.Shared.Setup;

public readonly struct PrerenderedResponsePersistenceOptions
{
    public bool IsEnabled { get; init; }
}

public interface IPrerenderedResponsePersistence : IResponsePersistence
{
    PrerenderedResponsePersistenceOptions Configure(IRequest request);
}
