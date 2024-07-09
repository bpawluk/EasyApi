namespace BlazorUtils.EasyApi.Client.Setup;

public class PrerenderedResponsePersistence : IClientResponsePersistence
{
    public ClientResponsePersistenceOptions Configure(IRequest _) => new() { UsePrerenderedResponses = true };
}
