using BlazorUtils.EasyApi.Shared.Setup;

namespace BlazorUtils.EasyApi.Client.Setup;

public class PrerenderedResponsePersistence : IPrerenderedResponsePersistence
{
    public PrerenderedResponsePersistenceOptions Configure(IRequest _) => new() { IsEnabled = true };
}
