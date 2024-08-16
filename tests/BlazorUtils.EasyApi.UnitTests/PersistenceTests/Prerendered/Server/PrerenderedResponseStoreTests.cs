using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Server.Setup;
using BlazorUtils.EasyApi.Shared.Persistence.Response;
using BlazorUtils.EasyApi.Shared.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace BlazorUtils.EasyApi.UnitTests.PersistenceTests.Prerendered.Server;

public class PrerenderedResponseStoreTests : PrerenderedResponseStoreTestsBase
{
    private readonly Mock<IInteractivityDetector> _interactivityDetectorMock = default!;

    public PrerenderedResponseStoreTests()
    {
        Services
            .AddEasyApi()
            .WithContract(GetType().Assembly)
            .WithServer()
            .Using<PrerenderedResponsePersistence>();

        _interactivityDetectorMock = new Mock<IInteractivityDetector>();

        _interactivityDetectorMock
            .Setup(detector => detector.IsInteractive)
            .Returns(true);

        Services.Replace(ServiceDescriptor.Singleton(_interactivityDetectorMock.Object));
    }

    [Fact]
    public void PrerenderedResponseStore_NoPersistedResponse_IsPrerendering_InnerCallerSucceeded_PersistsItsResponse()
    {
        _interactivityDetectorMock
            .Setup(detector => detector.IsInteractive)
            .Returns(false);

        var renderedComponent = RenderComponent<PrerenderedResponseStoreTestsComponent>();
        _persistentComponentState.TriggerOnPersisting();

        AssertCorrectResponse(renderedComponent, _responseProvider.Response);

        var isPersisted = _persistentComponentState.TryTake<ResponseSnapshot<string>>(StorageKey, out var responseSnapshot);
        Assert.True(isPersisted);
        Assert.NotNull(responseSnapshot);
        Assert.Equal(_responseProvider.Response.StatusCode, responseSnapshot.StatusCode);
        Assert.Equal(_responseProvider.Response.Response, responseSnapshot.Response);
    }

    [Fact]
    public void PrerenderedResponseStore_NoPersistedResponse_IsPrerendering_InnerCallerFailed_DoesNotPersist()
    {
        _interactivityDetectorMock
            .Setup(detector => detector.IsInteractive)
            .Returns(false);

        var innerCallerResponse = HttpResult<string>.BadRequest();
        _responseProvider.Response = innerCallerResponse;

        var renderedComponent = RenderComponent<PrerenderedResponseStoreTestsComponent>();
        _persistentComponentState.TriggerOnPersisting();

        AssertCorrectResponse(renderedComponent, innerCallerResponse);

        var isPersisted = _persistentComponentState.TryTake<ResponseSnapshot<string>>(StorageKey, out var _);
        Assert.False(isPersisted);
    }

    [Fact]
    public void PrerenderedResponseStore_NoPersistedResponse_NotPrerendering_InnerCallerSucceeded_DoesNotPersist()
    {
        var renderedComponent = RenderComponent<PrerenderedResponseStoreTestsComponent>();
        _persistentComponentState.TriggerOnPersisting();

        AssertCorrectResponse(renderedComponent, _responseProvider.Response);

        var isPersisted = _persistentComponentState.TryTake<ResponseSnapshot<string>>(StorageKey, out var _);
        Assert.False(isPersisted);
    }

    [Fact]
    public void PrerenderedResponseStore_NoPersistedResponse_NotPrerendering_InnerCallerFailed_DoesNotPersist()
    {
        var innerCallerResponse = HttpResult<string>.BadRequest();
        _responseProvider.Response = innerCallerResponse;

        var renderedComponent = RenderComponent<PrerenderedResponseStoreTestsComponent>();
        _persistentComponentState.TriggerOnPersisting();

        AssertCorrectResponse(renderedComponent, innerCallerResponse);

        var isPersisted = _persistentComponentState.TryTake<ResponseSnapshot<string>>(StorageKey, out var _);
        Assert.False(isPersisted);
    }
}
