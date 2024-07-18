using BlazorUtils.EasyApi.Shared.Setup;

namespace BlazorUtils.EasyApi.Client.Setup;

public class InMemoryResponsePersistence : IInMemoryResponsePersistence
{
    public InMemoryResponsePersistenceOptions Configure(IRequest _) => new() { IsEnabled = true };
}
