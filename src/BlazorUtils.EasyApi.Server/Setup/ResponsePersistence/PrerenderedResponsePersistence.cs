namespace BlazorUtils.EasyApi.Server.Setup;

public class PrerenderedResponsePersistence : IServerResponsePersistence
{
    public ServerResponsePersistenceOptions Configure(IRequest _) => new() { UsePrerenderedResponses = true };
}
