using BlazorUtils.EasyApi.Shared.Setup;

namespace BlazorUtils.EasyApi.Server.Setup;

public class InMemoryResponsePersistence : IInMemoryResponsePersistence
{
    public InMemoryResponsePersistenceOptions Configure(IRequest _) => new() { IsEnabled = true };
}
