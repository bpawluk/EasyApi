using BlazorUtils.EasyApi.Shared.Rendering;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace BlazorUtils.EasyApi.UnitTests.MemoryTests;

public abstract class InMemoryResponseStoreTestsBase : TestContext
{
    public const string StorageKey = "storage-key";

    protected readonly InnerCallerResponseProvider _innerCallerResponseProvider;
    internal readonly Mock<IInteractivityDetector> _interactivityDetectorMock;

    public InMemoryResponseStoreTestsBase()
    {
        _innerCallerResponseProvider = new();
        Services.AddSingleton(_innerCallerResponseProvider);

        _interactivityDetectorMock = new Mock<IInteractivityDetector>();
    }

    [Fact]
    public async Task InMemoryResponseStore_BlazorInteractive_WithPersistedResponse_RetrievesTheResponse()
    {
        _interactivityDetectorMock.Setup(detector => detector.IsInteractive).Returns(true);

        var persistedResponse = HttpResult<string>.Ok("persisted-response");
        _innerCallerResponseProvider.Response = persistedResponse;

        var caller = Services.GetRequiredService<IPersistentCall<InMemoryResponseStoreTestsRequest, string>>();
        await caller.CallHttp(StorageKey, new());

        _innerCallerResponseProvider.Response = default!;

        var renderedComponent = RenderComponent<InMemoryResponseStoreTestsComponent>();

        AssertCorrectResponse(renderedComponent, persistedResponse);
    }

    [Fact]
    public void InMemoryResponseStore_BlazorInteractive_NoPersistedResponse_DoesNotRetrieve()
    {
        _interactivityDetectorMock.Setup(detector => detector.IsInteractive).Returns(true);

        var innerCallerResponse = HttpResult<string>.Ok("inner-caller-response");
        _innerCallerResponseProvider.Response = innerCallerResponse;

        var renderedComponent = RenderComponent<InMemoryResponseStoreTestsComponent>();

        AssertCorrectResponse(renderedComponent, innerCallerResponse);
    }

    [Fact]
    public async Task InMemoryResponseStore_BlazorStatic_WithPersistedResponse_DoesNotRetrieve()
    {
        var persistedResponse = HttpResult<string>.Ok("persisted-response");
        _innerCallerResponseProvider.Response = persistedResponse;

        var caller = Services.GetRequiredService<IPersistentCall<InMemoryResponseStoreTestsRequest, string>>();
        await caller.CallHttp(StorageKey, new());

        _innerCallerResponseProvider.Response = default!;

        var innerCallerResponse = HttpResult<string>.Ok("inner-caller-response");
        _innerCallerResponseProvider.Response = innerCallerResponse;

        var renderedComponent = RenderComponent<InMemoryResponseStoreTestsComponent>();

        AssertCorrectResponse(renderedComponent, innerCallerResponse);
    }

    [Fact]
    public void InMemoryResponseStore_BlazorStatic_NoPersistedResponse_DoesNotRetrieve()
    {
        var innerCallerResponse = HttpResult<string>.Ok("inner-caller-response");
        _innerCallerResponseProvider.Response = innerCallerResponse;

        var renderedComponent = RenderComponent<InMemoryResponseStoreTestsComponent>();

        AssertCorrectResponse(renderedComponent, innerCallerResponse);
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

public class InnerCallerResponseProvider
{
    public HttpResult<string> Response { get; set; } = default!;
}

