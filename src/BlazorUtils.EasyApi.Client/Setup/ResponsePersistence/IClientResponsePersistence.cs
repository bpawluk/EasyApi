namespace BlazorUtils.EasyApi.Client.Setup;

public interface IClientResponsePersistence
{
    ClientResponsePersistenceOptions Configure(IRequest request);
}
