using BlazorUtils.EasyApi.Shared.Rendering;
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

internal class InMemoryResponseStoreTestsRequestHandler(TestResponseProvider responseProvider) : TestRequestHandler<InMemoryResponseStoreTestsRequest>(responseProvider) { }


