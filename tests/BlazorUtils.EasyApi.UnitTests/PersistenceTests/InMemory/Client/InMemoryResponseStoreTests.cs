using BlazorUtils.EasyApi.Client;
using BlazorUtils.EasyApi.Client.Setup;
using BlazorUtils.EasyApi.UnitTests.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BlazorUtils.EasyApi.UnitTests.PersistenceTests.InMemory.Client;

public class InMemoryResponseStoreTests : InMemoryResponseStoreTestsBase
{
    public InMemoryResponseStoreTests()
    {
        Services
            .AddEasyApi()
            .WithContract(GetType().Assembly)
            .WithClient()
            .Using<TestHttpClientProvider>()
            .Using<InMemoryResponsePersistence>();

        Services.Replace(ServiceDescriptor.Singleton(_interactivityDetectorMock.Object));
    }
}
