using System.Net;
using System.Text;
using System.Text.Json;
using BlazorUtils.EasyApi.Client;
using BlazorUtils.EasyApi.Client.Setup;
using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Shared.Setup;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace BlazorUtils.EasyApi.UnitTests.SerializationTests.Client;

public sealed class SerializationSetupTests : IDisposable
{
    private ServiceProvider _sut = default!;
    private readonly Mock<HttpClient> _httpClientMock = new();

    private void Initialize(Action<JsonSerializerOptions> serializationConfig)
    {
        var services = new ServiceCollection();

        var httpClientProviderMock = new Mock<IHttpClientProvider>();
        httpClientProviderMock.Setup(x => x.GetClient(It.IsAny<IRequest>())).Returns(_httpClientMock.Object);

        var easyApiBuilder = services
            .AddEasyApi()
            .ConfigureSerialization(serializationConfig)
            .WithContract(GetType().Assembly)
            .WithClient()
            .Using(httpClientProviderMock.Object);

        _sut = services.BuildServiceProvider();
    }

    [Fact]
    public async Task DeserializingResponse_WithMatchingSerializationConfiguration_ReadsTheContentCorrectly()
    {
        // Arrange
        var serializationConfig = new Action<JsonSerializerOptions>(options =>
        {
            options.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseUpper;
            options.IndentCharacter = '\t';
            options.WriteIndented = true;
        });
        Initialize(serializationConfig);

        var responseToSerialize = new SerializationSetupTestsRequest.Response(
            Guid.NewGuid(),
            DateTime.UtcNow,
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
            [1, 2, 3],
            [
                new SerializationSetupTestsRequest.Flag(Guid.NewGuid(), true),
                new SerializationSetupTestsRequest.Flag(Guid.NewGuid(), false),
                new SerializationSetupTestsRequest.Flag(Guid.NewGuid(), null)
            ]);

        var serializationOptions = new JsonSerializerOptions();
        serializationConfig(serializationOptions);
        var serializedResponse = JsonSerializer.Serialize(responseToSerialize, serializationOptions);

        _httpClientMock
            .Setup(x => x.SendAsync(
                It.IsAny<HttpRequestMessage>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(serializedResponse, Encoding.UTF8, "application/json")
            });

        // Act
        var client = _sut.GetRequiredService<ICall<SerializationSetupTestsRequest, SerializationSetupTestsRequest.Response>>();
        var deserializedResponse = await client.Call(new SerializationSetupTestsRequest());

        // Assert
        Assert.Equal(responseToSerialize.Id, deserializedResponse.Id);
        Assert.Equal(responseToSerialize.Time, deserializedResponse.Time);
        Assert.Equal(responseToSerialize.Text, deserializedResponse.Text);
        Assert.Equal(responseToSerialize.Numbers, deserializedResponse.Numbers);
        Assert.Equal(responseToSerialize.Flags, deserializedResponse.Flags);
    }

    [Fact]
    public async Task DeserializingResponse_WithDifferentSerializationConfiguration_DoesNotReadTheContentCorrectly()
    {
        // Arrange
        var serializationConfig = new Action<JsonSerializerOptions>(options =>
        {
            options.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseUpper;
            options.IndentCharacter = '\t';
            options.WriteIndented = true;
        });
        Initialize(options => { });

        var responseToSerialize = new SerializationSetupTestsRequest.Response(
            Guid.NewGuid(),
            DateTime.UtcNow,
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
            [1, 2, 3],
            [
                new SerializationSetupTestsRequest.Flag(Guid.NewGuid(), true),
                new SerializationSetupTestsRequest.Flag(Guid.NewGuid(), false),
                new SerializationSetupTestsRequest.Flag(Guid.NewGuid(), null)
            ]);

        var serializationOptions = new JsonSerializerOptions();
        serializationConfig(serializationOptions);
        var serializedResponse = JsonSerializer.Serialize(responseToSerialize, serializationOptions);

        _httpClientMock
            .Setup(x => x.SendAsync(
                It.IsAny<HttpRequestMessage>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(serializedResponse, Encoding.UTF8, "application/json")
            });

        // Act
        var client = _sut.GetRequiredService<ICall<SerializationSetupTestsRequest, SerializationSetupTestsRequest.Response>>();
        var deserializedResponse = await client.Call(new SerializationSetupTestsRequest());

        // Assert
        Assert.Equal(default, deserializedResponse.Id);
        Assert.Equal(default, deserializedResponse.Time);
        Assert.Equal(default, deserializedResponse.Text);
        Assert.Equal(default, deserializedResponse.Numbers);
        Assert.Equal(default, deserializedResponse.Flags);
    }

    public void Dispose() => _sut.Dispose();
}
