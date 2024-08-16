using BlazorUtils.EasyApi.Shared.Persistence.Response;
using BlazorUtils.EasyApi.UnitTests.Utils;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorUtils.EasyApi.UnitTests.PersistenceTests.Prerendered;

public abstract class PrerenderedResponseStoreTestsBase : TestContext
{
    public const string StorageKey = "storage-key";

    protected readonly TestResponseProvider _responseProvider;
    protected readonly FakePersistentComponentState _persistentComponentState;

    public PrerenderedResponseStoreTestsBase()
    {
        _responseProvider = new();
        Services.AddSingleton(_responseProvider);

        _persistentComponentState = this.AddFakePersistentComponentState();
    }

    [Fact]
    public void PrerenderedResponseStore_WithPersistedResponse_RetrievesTheResponse()
    {
        var persistedResponse = HttpResult<string>.Ok("persisted-response");
        _persistentComponentState.Persist(StorageKey, new ResponseSnapshot<string>(persistedResponse.StatusCode, persistedResponse.Response!));

        var renderedComponent = RenderComponent<PrerenderedResponseStoreTestsComponent>();

        AssertCorrectResponse(renderedComponent, persistedResponse);
    }

    [Fact]
    public void PrerenderedResponseStore_NoPersistedResponse_DoesNotRetrieve()
    {
        var renderedComponent = RenderComponent<PrerenderedResponseStoreTestsComponent>();
        AssertCorrectResponse(renderedComponent, _responseProvider.Response);
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

internal class PrerenderedResponseStoreTestsRequestHandler(TestResponseProvider responseProvider) : TestRequestHandler<PrerenderedResponseStoreTestsRequest>(responseProvider) { }
