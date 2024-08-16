using BlazorUtils.EasyApi.Shared.Persistence;
using BlazorUtils.EasyApi.Shared.Persistence.Response;
using BlazorUtils.EasyApi.UnitTests.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using System.Net;

namespace BlazorUtils.EasyApi.UnitTests.PersistenceTests.Caller;

public abstract class PersistentCallerTestsBase : IAsyncLifetime
{
    public const string _storageKey = "storage-key";

    private IServiceProvider _sut = default!;
    private Mock<IResponseStore<string>> _responseStoreMock = default!;
    private readonly TestResponseProvider _responseProvider = new();

    public async Task InitializeAsync()
    {
        _responseStoreMock = new Mock<IResponseStore<string>>();

        var responseStoreFactoryMock = new Mock<IResponseStoreFactory>();
        responseStoreFactoryMock
            .Setup(factory => factory.GetStore(It.IsAny<PersistentCallerTestsRequest>()))
            .Returns(_responseStoreMock.Object);

        _sut = await CreateSUT(services =>
        {
            services.Replace(ServiceDescriptor.Singleton(responseStoreFactoryMock.Object));
            services.AddSingleton(_responseProvider);
        });
    }

    [Fact]
    public async Task PersistentCaller_WithPersistedResponse_ReturnsTheResponse()
    {
        var persistedResponse = HttpResult<string>.Ok("persisted-response");
        _responseStoreMock
            .Setup(store => store.Retrieve(_storageKey))
            .Returns(PersistedResponse<string>.Create(persistedResponse));

        var caller = _sut.GetRequiredService<IPersistentCall<PersistentCallerTestsRequest, string>>();
        var result = await caller.CallHttp(_storageKey, new());

        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(persistedResponse.Response, result.Response);
        _responseStoreMock.Verify(store => store.Save(_storageKey, It.IsAny<HttpResult<string>>()), Times.Never);
    }

    [Fact]
    public async Task PersistentCaller_WithPersistedResponse_FreshCallForced_ReturnsInnerResponse_AndPersistsIt()
    {
        var persistedResponse = HttpResult<string>.Ok("persisted-response");
        _responseStoreMock
            .Setup(store => store.Retrieve(_storageKey))
            .Returns(PersistedResponse<string>.Create(persistedResponse));

        var caller = _sut.GetRequiredService<IPersistentCall<PersistentCallerTestsRequest, string>>();
        var result = await caller.CallHttp(_storageKey, new(), true);

        Assert.Equal(_responseProvider.Response.StatusCode, result.StatusCode);
        Assert.Equal(_responseProvider.Response.Response, result.Response);
        _responseStoreMock.Verify(store => store.Save(_storageKey, It.IsAny<HttpResult<string>>()), Times.Once);
    }

    [Fact]
    public async Task PersistentCaller_WithPersistedStickyResponse_ReturnsTheResponse_AndPersistsIt()
    {
        var persistedResponse = HttpResult<string>.Ok("persisted-response");
        _responseStoreMock
            .Setup(store => store.Retrieve(_storageKey))
            .Returns(PersistedResponse<string>.Sticky(persistedResponse));

        var caller = _sut.GetRequiredService<IPersistentCall<PersistentCallerTestsRequest, string>>();
        var result = await caller.CallHttp(_storageKey, new());

        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(persistedResponse.Response, result.Response);
        _responseStoreMock.Verify(store => store.Save(_storageKey, It.IsAny<HttpResult<string>>()), Times.Once);
    }

    [Fact]
    public async Task PersistentCaller_NoPersistedResponse_InnerCallerSucceeded_ReturnsInnerResponse_AndPersistsIt()
    {
        var caller = _sut.GetRequiredService<IPersistentCall<PersistentCallerTestsRequest, string>>();
        var result = await caller.CallHttp(_storageKey, new());

        Assert.Equal(_responseProvider.Response.StatusCode, result.StatusCode);
        Assert.Equal(_responseProvider.Response.Response, result.Response);
        _responseStoreMock.Verify(store => store.Save(_storageKey, It.IsAny<HttpResult<string>>()), Times.Once);
    }

    [Fact]
    public async Task PersistentCaller_NoPersistedResponse_InnerCallerFailed_ReturnsInnerResponse_DoesNotPersistIt()
    {
        var innerCallerResponse = HttpResult<string>.BadRequest();
        _responseProvider.Response = innerCallerResponse;

        var caller = _sut.GetRequiredService<IPersistentCall<PersistentCallerTestsRequest, string>>();
        var result = await caller.CallHttp(_storageKey, new());

        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Null(result.Response);
        _responseStoreMock.Verify(store => store.Save(_storageKey, It.IsAny<HttpResult<string>>()), Times.Never);
    }

    protected abstract Task<IServiceProvider> CreateSUT(Action<IServiceCollection> servicesOverride);

    public abstract Task DisposeAsync();
}

[Route(nameof(PersistentCallerTestsRequest))]
public class PersistentCallerTestsRequest : IGet<string> { }

internal class PersistentCallerTestsRequestHandler(TestResponseProvider responseProvider) : TestRequestHandler<PersistentCallerTestsRequest>(responseProvider) { }
