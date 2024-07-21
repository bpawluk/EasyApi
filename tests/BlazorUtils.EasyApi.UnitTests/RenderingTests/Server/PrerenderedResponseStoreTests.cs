using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Server.Setup;
using BlazorUtils.EasyApi.Shared.Persistence;
using BlazorUtils.EasyApi.Shared.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace BlazorUtils.EasyApi.UnitTests.RenderingTests.Server;

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

        var innerCallerResponse = HttpResult<string>.Ok("inner-caller-response");
        _innerCallerResponseProvider.Response = innerCallerResponse;

        var renderedComponent = RenderComponent<PrerenderedResponseStoreTestsComponent>();
        _persistentComponentState.TriggerOnPersisting();

        AssertCorrectResponse(renderedComponent, innerCallerResponse);

        var isPersisted = _persistentComponentState.TryTake<ResponseSnapshot<string>>(StorageKey, out var responseSnapshot);
        Assert.True(isPersisted);
        Assert.NotNull(responseSnapshot);
        Assert.Equal(innerCallerResponse.StatusCode, responseSnapshot.StatusCode);
        Assert.Equal(innerCallerResponse.Response, responseSnapshot.Response);
    }

    [Fact]
    public void PrerenderedResponseStore_NoPersistedResponse_IsPrerendering_InnerCallerFailed_DoesNotPersist()
    {
        _interactivityDetectorMock
            .Setup(detector => detector.IsInteractive)
            .Returns(false);

        var innerCallerResponse = HttpResult<string>.BadRequest();
        _innerCallerResponseProvider.Response = innerCallerResponse;

        var renderedComponent = RenderComponent<PrerenderedResponseStoreTestsComponent>();
        _persistentComponentState.TriggerOnPersisting();

        AssertCorrectResponse(renderedComponent, innerCallerResponse);

        var isPersisted = _persistentComponentState.TryTake<ResponseSnapshot<string>>(StorageKey, out var _);
        Assert.False(isPersisted);
    }

    [Fact]
    public void PrerenderedResponseStore_NoPersistedResponse_NotPrerendering_InnerCallerSucceeded_DoesNotPersist()
    {
        var innerCallerResponse = HttpResult<string>.Ok("inner-caller-response");
        _innerCallerResponseProvider.Response = innerCallerResponse;

        var renderedComponent = RenderComponent<PrerenderedResponseStoreTestsComponent>();
        _persistentComponentState.TriggerOnPersisting();

        AssertCorrectResponse(renderedComponent, innerCallerResponse);

        var isPersisted = _persistentComponentState.TryTake<ResponseSnapshot<string>>(StorageKey, out var _);
        Assert.False(isPersisted);
    }

    [Fact]
    public void PrerenderedResponseStore_NoPersistedResponse_NotPrerendering_InnerCallerFailed_DoesNotPersist()
    {
        var innerCallerResponse = HttpResult<string>.BadRequest();
        _innerCallerResponseProvider.Response = innerCallerResponse;

        var renderedComponent = RenderComponent<PrerenderedResponseStoreTestsComponent>();
        _persistentComponentState.TriggerOnPersisting();

        AssertCorrectResponse(renderedComponent, innerCallerResponse);

        var isPersisted = _persistentComponentState.TryTake<ResponseSnapshot<string>>(StorageKey, out var _);
        Assert.False(isPersisted);
    } 
}

internal class PrerenderedResponseStoreTestsRequestHandler(InnerCallerResponseProvider responseProvider) 
    : IHandle<PrerenderedResponseStoreTestsRequest, string>
{
    private readonly InnerCallerResponseProvider _innerCallerResponseProvider = responseProvider;

    public Task<HttpResult<string>> Handle(PrerenderedResponseStoreTestsRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_innerCallerResponseProvider.Response);
    }
}
