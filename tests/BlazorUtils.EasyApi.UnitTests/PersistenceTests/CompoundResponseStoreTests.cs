using BlazorUtils.EasyApi.Shared.Persistence;
using BlazorUtils.EasyApi.Shared.Persistence.Compound;
using BlazorUtils.EasyApi.Shared.Persistence.Response;
using Moq;
using System.Net;

namespace BlazorUtils.EasyApi.UnitTests.PersistenceTests;

public class CompoundResponseStoreTests
{
    private const string _storageKey = "storage-key";

    private readonly CompoundResponseStore<string> _sut;

    private readonly Mock<IResponseStore<string>> _firstStore = new();
    private readonly Mock<IResponseStore<string>> _secondStore = new();
    private readonly Mock<IResponseStore<string>> _thirdStore = new();

    public CompoundResponseStoreTests()
    {
        _sut = new CompoundResponseStore<string>([_firstStore.Object, _secondStore.Object, _thirdStore.Object]);
    }

    [Fact]
    public void Save_ShouldSaveToAllStores()
    {
        var response = HttpResult<string>.Ok("response-to-save");

        _sut.Save(_storageKey, response);

        _firstStore.Verify(x => x.Save(_storageKey, response), Times.Once);
        _secondStore.Verify(x => x.Save(_storageKey, response), Times.Once);
        _thirdStore.Verify(x => x.Save(_storageKey, response), Times.Once);
    }

    [Theory]
    [InlineData(null, null, null)]
    [InlineData("first-response", null, null)]
    [InlineData(null, "second-response", null)]
    [InlineData(null, null, "third-response")]
    [InlineData("first-response", "second-response", null)]
    [InlineData(null, "second-response", "third-response")]
    [InlineData("first-response", null, "third-response")]
    [InlineData("first-response", "second-response", "third-response")]
    public void Retrieve_ShouldReturnFirstResponse(string? firstResponse, string? secondResponse, string? thirdResponse)
    {
        var expectedResponse = firstResponse ?? secondResponse ?? thirdResponse;

        if (firstResponse is not null)
        {
            _firstStore
                .Setup(x => x.Retrieve(_storageKey))
                .Returns(PersistedResponse<string>.Create(HttpResult<string>.Ok(firstResponse)));
        }
        
        if (secondResponse is not null)
        {
            _secondStore
                .Setup(x => x.Retrieve(_storageKey))
                .Returns(PersistedResponse<string>.Create(HttpResult<string>.Ok(secondResponse)));
        }

        if (thirdResponse is not null)
        {
            _thirdStore
                .Setup(x => x.Retrieve(_storageKey))
                .Returns(PersistedResponse<string>.Create(HttpResult<string>.Ok(thirdResponse)));
        }

        var result = _sut.Retrieve(_storageKey);

        if (expectedResponse is null)
        {
           Assert.Null(result);
        }
        else
        {
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, result.Value.StatusCode);
            Assert.Equal(expectedResponse, result.Value.Response);
        }
    }
}
