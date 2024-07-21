using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Server.Setup;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BlazorUtils.EasyApi.UnitTests.MemoryTests.Server;

public class InMemoryResponseStoreTests : InMemoryResponseStoreTestsBase
{
    public InMemoryResponseStoreTests()
    {
        Services
            .AddEasyApi()
            .WithContract(GetType().Assembly)
            .WithServer()
            .Using<InMemoryResponsePersistence>();

        Services.Replace(ServiceDescriptor.Singleton(_interactivityDetectorMock.Object));
    }
}

internal class InMemoryResponseStoreTestsRequestHandler(InnerCallerResponseProvider responseProvider) 
    : IHandle<InMemoryResponseStoreTestsRequest, string>
{
    private readonly InnerCallerResponseProvider _innerCallerResponseProvider = responseProvider;

    public Task<HttpResult<string>> Handle(InMemoryResponseStoreTestsRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_innerCallerResponseProvider.Response);
    }
}
