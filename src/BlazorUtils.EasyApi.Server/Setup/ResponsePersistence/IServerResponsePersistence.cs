namespace BlazorUtils.EasyApi.Server.Setup;

public interface IServerResponsePersistence
{
    ServerResponsePersistenceOptions Configure(IRequest request);
}
