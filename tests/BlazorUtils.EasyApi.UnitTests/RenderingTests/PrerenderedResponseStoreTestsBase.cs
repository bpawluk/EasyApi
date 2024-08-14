using BlazorUtils.EasyApi.Shared.Persistence.Response;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorUtils.EasyApi.UnitTests.RenderingTests;

public abstract class PrerenderedResponseStoreTestsBase : TestContext
{
    public const string StorageKey = "storage-key";

    protected readonly InnerCallerResponseProvider _innerCallerResponseProvider;
    protected readonly FakePersistentComponentState _persistentComponentState;

    public PrerenderedResponseStoreTestsBase()
    {
        _innerCallerResponseProvider = new();
        Services.AddSingleton(_innerCallerResponseProvider);

        _persistentComponentState = this.AddFakePersistentComponentState();
    }

    [Fact]
    public void PrerenderedResponseStore_WithPersistedResponse_RetrievesTheResponse()
    {
        var persistedResponse = HttpResult<string>.Ok("persisted-response");
        _persistentComponentState.Persist(
            StorageKey, 
            new ResponseSnapshot<string>(
                persistedResponse.StatusCode, 
                persistedResponse.Response!));

        var renderedComponent = RenderComponent<PrerenderedResponseStoreTestsComponent>();

        AssertCorrectResponse(renderedComponent, persistedResponse);
    }

    [Fact]
    public void PrerenderedResponseStore_NoPersistedResponse_DoesNotRetrieve()
    {
        var innerCallerResponse = HttpResult<string>.Ok("inner-caller-response");
        _innerCallerResponseProvider.Response = innerCallerResponse;

        var renderedComponent = RenderComponent<PrerenderedResponseStoreTestsComponent>();

        AssertCorrectResponse(renderedComponent, innerCallerResponse);
    }

    protected static void AssertCorrectResponse(
        IRenderedComponent<PrerenderedResponseStoreTestsComponent> renderedComponent, 
        HttpResult<string> expectedResponse)
    {
        var statusCodeElement = renderedComponent.Find("#statusCodeElement");
        Assert.Equal(expectedResponse.StatusCode.ToString(), statusCodeElement.TextContent);

        var responseElement = renderedComponent.Find("#responseElement");
        Assert.Equal(expectedResponse.Response ?? string.Empty, responseElement.TextContent);
    }
}

[Route(nameof(PrerenderedResponseStoreTestsRequest))]
public class PrerenderedResponseStoreTestsRequest : IGet<string> { }

public class InnerCallerResponseProvider
{
    public HttpResult<string> Response { get; set; } = default!;
}

