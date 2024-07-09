namespace BlazorUtils.EasyApi.Server.Setup;

internal class NoResponsePersistence : IServerResponsePersistence
{
    public ServerResponsePersistenceOptions Configure(IRequest _) => new();
}
