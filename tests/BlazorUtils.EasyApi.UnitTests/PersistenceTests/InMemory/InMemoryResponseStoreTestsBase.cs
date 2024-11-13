using BlazorUtils.EasyApi.Shared.Rendering;
using BlazorUtils.EasyApi.Shared.Setup;
using BlazorUtils.EasyApi.UnitTests.Utils;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace BlazorUtils.EasyApi.UnitTests.PersistenceTests.InMemory;

public abstract class InMemoryResponseStoreTestsBase : TestContext
{
    public const string StorageKey = "storage-key";

    protected readonly TestResponseProvider _responseProvider;
    internal readonly Mock<IInteractivityDetector> _interactivityDetectorMock;

    public InMemoryResponseStoreTestsBase()
    {
        _responseProvider = new();
        Services.AddSingleton(_responseProvider);

        _interactivityDetectorMock = new Mock<IInteractivityDetector>();
    }

    [Fact]
    public async Task InMemoryResponseStore_BlazorInteractive_WithPersistedResponse_RetrievesTheResponse()
    {
        _interactivityDetectorMock.Setup(detector => detector.IsInteractive).Returns(true);

        var persistedResponse = HttpResult<string>.Ok("persisted-response");
        _responseProvider.Response = persistedResponse;

        var caller = Services.GetRequiredService<IPersistentCall<InMemoryResponseStoreTestsRequest, string>>();
        await caller.CallHttp(StorageKey, new());

        _responseProvider.Response = HttpResult<string>.Ok("inner-caller-response");

        var renderedComponent = RenderComponent<InMemoryResponseStoreTestsComponent>();

        AssertCorrectResponse(renderedComponent, persistedResponse);
    }

    [Fact]
    public async Task InMemoryResponseStore_BlazorInteractive_WithPersistedResponse_AndAbsoluteExpiration_RetrievesTheResponse()
    {
        _interactivityDetectorMock.Setup(detector => detector.IsInteractive).Returns(true);

        var persistedResponse = HttpResult<string>.Ok("persisted-response");
        _responseProvider.Response = persistedResponse;

        var caller = Services.GetRequiredService<IPersistentCall<InMemoryResponseStoreWithAbsoluteExpirationTestsRequest, string>>();
        await caller.CallHttp(StorageKey, new());

        _responseProvider.Response = HttpResult<string>.Ok("inner-caller-response");

        var renderedComponent = RenderComponent<InMemoryResponseStoreTestsComponent>();
        AssertCorrectResponse(renderedComponent, persistedResponse);

        await Task.Delay(1000);

        renderedComponent = RenderComponent<InMemoryResponseStoreTestsComponent>();
        AssertCorrectResponse(renderedComponent, _responseProvider.Response);
    }

    [Fact]
    public async Task InMemoryResponseStore_BlazorInteractive_WithPersistedResponse_AndSlidingExpiration_RetrievesTheResponse()
    {
        _interactivityDetectorMock.Setup(detector => detector.IsInteractive).Returns(true);

        var persistedResponse = HttpResult<string>.Ok("persisted-response");
        _responseProvider.Response = persistedResponse;

        var caller = Services.GetRequiredService<IPersistentCall<InMemoryResponseStoreWithSlidingExpirationTestsRequest, string>>();
        await caller.CallHttp(StorageKey, new());

        _responseProvider.Response = HttpResult<string>.Ok("inner-caller-response");

        await Task.Delay(200);

        var renderedComponent = RenderComponent<InMemoryResponseStoreTestsComponent>();
        AssertCorrectResponse(renderedComponent, persistedResponse);

        await Task.Delay(200);

        renderedComponent = RenderComponent<InMemoryResponseStoreTestsComponent>();
        AssertCorrectResponse(renderedComponent, persistedResponse);

        await Task.Delay(500);

        renderedComponent = RenderComponent<InMemoryResponseStoreTestsComponent>();
        AssertCorrectResponse(renderedComponent, _responseProvider.Response);
    }

    [Fact]
    public void InMemoryResponseStore_BlazorInteractive_NoPersistedResponse_DoesNotRetrieve()
    {
        _interactivityDetectorMock.Setup(detector => detector.IsInteractive).Returns(true);

        var renderedComponent = RenderComponent<InMemoryResponseStoreTestsComponent>();

        AssertCorrectResponse(renderedComponent, _responseProvider.Response);
    }

    [Fact]
    public async Task InMemoryResponseStore_BlazorStatic_WithPersistedResponse_DoesNotRetrieve()
    {
        _responseProvider.Response = HttpResult<string>.Ok("persisted-response");

        var caller = Services.GetRequiredService<IPersistentCall<InMemoryResponseStoreTestsRequest, string>>();
        await caller.CallHttp(StorageKey, new());

        _responseProvider.Response = HttpResult<string>.Ok("inner-caller-response");

        var renderedComponent = RenderComponent<InMemoryResponseStoreTestsComponent>();

        AssertCorrectResponse(renderedComponent, _responseProvider.Response);
    }

    [Fact]
    public void InMemoryResponseStore_BlazorStatic_NoPersistedResponse_DoesNotRetrieve()
    {
        var renderedComponent = RenderComponent<InMemoryResponseStoreTestsComponent>();
        AssertCorrectResponse(renderedComponent, _responseProvider.Response);
    }

    protected static void AssertCorrectResponse(
        IRenderedComponent<InMemoryResponseStoreTestsComponent> renderedComponent,
        HttpResult<string> expectedResponse)
    {
        var statusCodeElement = renderedComponent.Find("#statusCodeElement");
        Assert.Equal(expectedResponse.StatusCode.ToString(), statusCodeElement.TextContent);

        var responseElement = renderedComponent.Find("#responseElement");
        Assert.Equal(expectedResponse.Response ?? string.Empty, responseElement.TextContent);
    }
}

[Route(nameof(InMemoryResponseStoreTestsRequest))]
public class InMemoryResponseStoreTestsRequest : IGet<string> { }

[Route(nameof(InMemoryResponseStoreWithAbsoluteExpirationTestsRequest))]
public class InMemoryResponseStoreWithAbsoluteExpirationTestsRequest : IGet<string> { }

[Route(nameof(InMemoryResponseStoreWithSlidingExpirationTestsRequest))]
public class InMemoryResponseStoreWithSlidingExpirationTestsRequest : IGet<string> { }

internal class InMemoryResponseStoreTestsRequestHandler(TestResponseProvider responseProvider) : TestRequestHandler<InMemoryResponseStoreTestsRequest>(responseProvider) { }

internal class InMemoryResponseStoreWithAbsoluteExpirationTestsRequestHandler(TestResponseProvider responseProvider) : TestRequestHandler<InMemoryResponseStoreWithAbsoluteExpirationTestsRequest>(responseProvider) { }

internal class InMemoryResponseStoreWithSlidingExpirationTestsRequestHandler(TestResponseProvider responseProvider) : TestRequestHandler<InMemoryResponseStoreWithSlidingExpirationTestsRequest>(responseProvider) { }

internal class TestInMemoryResponsePersistence : IInMemoryResponsePersistence
{
    public InMemoryResponsePersistenceOptions Configure(IRequest request) => request switch
    {
        InMemoryResponseStoreWithAbsoluteExpirationTestsRequest _ => new() { IsEnabled = true, AbsoluteExpiration = TimeSpan.FromMilliseconds(1000) },
        InMemoryResponseStoreWithSlidingExpirationTestsRequest _ => new() { IsEnabled = true, SlidingExpiration = TimeSpan.FromMilliseconds(500) },
        _ => new() { IsEnabled = true },
    };
}
