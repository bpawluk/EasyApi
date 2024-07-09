namespace BlazorUtils.EasyApi.Client.Setup;

internal class NoResponsePersistence : IClientResponsePersistence
{
    public ClientResponsePersistenceOptions Configure(IRequest _) => new();
}
