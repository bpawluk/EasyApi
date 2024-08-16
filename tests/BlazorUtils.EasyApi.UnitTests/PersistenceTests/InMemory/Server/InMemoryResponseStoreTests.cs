using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Server.Setup;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BlazorUtils.EasyApi.UnitTests.PersistenceTests.InMemory.Server;

public class InMemoryResponseStoreTests : InMemoryResponseStoreTestsBase
{
    public InMemoryResponseStoreTests()
    {
        Services
            .AddEasyApi()
            .WithContract(GetType().Assembly)
            .WithServer()
            .Using<TestInMemoryResponsePersistence>();

        Services.Replace(ServiceDescriptor.Singleton(_interactivityDetectorMock.Object));
    }
}
