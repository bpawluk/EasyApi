using BlazorUtils.EasyApi.Client;
using BlazorUtils.EasyApi.Client.Setup;
using BlazorUtils.EasyApi.UnitTests.Utils;

namespace BlazorUtils.EasyApi.UnitTests.PersistenceTests.Prerendered.Client;

public class PrerenderedResponseStoreTests : PrerenderedResponseStoreTestsBase
{
    public PrerenderedResponseStoreTests()
    {
        Services
            .AddEasyApi()
            .WithContract(GetType().Assembly)
            .WithClient()
            .Using<TestHttpClientProvider>()
            .Using<PrerenderedResponsePersistence>();
    }
}
