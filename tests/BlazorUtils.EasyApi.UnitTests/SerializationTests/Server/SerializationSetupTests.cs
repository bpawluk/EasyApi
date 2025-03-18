using System.Net;
using System.Text.Json;
using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Shared.Setup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;

namespace BlazorUtils.EasyApi.UnitTests.SerializationTests.Server;

public sealed class SerializationSetupTests : IAsyncDisposable
{
    private WebApplication _sut = default!;

    private async Task Initialize(Action<JsonSerializerOptions> serializationConfig)
    {
        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseTestServer();

        builder.Services
            .AddEasyApi()
            .ConfigureSerialization(serializationConfig)
            .WithContract(GetType().Assembly)
            .WithServer();

        _sut = builder.Build();
        _sut.MapRequests();

        await _sut.StartAsync();
    }

    [Fact]
    public async Task SerializingResponse_WithMatchingSerializationConfiguration_WritesTheContentCorrectly()
    {
        // Arrange
        var serializationConfig = new Action<JsonSerializerOptions>(options =>
        {
            options.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseUpper;
            options.IndentCharacter = '\t';
            options.WriteIndented = true;
        });
        await Initialize(serializationConfig);

        var serializationOptions = new JsonSerializerOptions();
        serializationConfig(serializationOptions);

        // Act
        var client = _sut.GetTestClient();
        var result = await client.GetAsync(nameof(SerializationSetupTestsRequest));

        var responseContent = await result.Content.ReadAsStringAsync();
        var deserializedResponse = JsonSerializer.Deserialize<SerializationSetupTestsRequest.Response>(responseContent, serializationOptions);

        // Assert
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.NotNull(deserializedResponse);
        Assert.Equal(SerializationSetupTestsRequestHandler.ReturnedResponse.Id, deserializedResponse.Id);
        Assert.Equal(SerializationSetupTestsRequestHandler.ReturnedResponse.Time, deserializedResponse.Time);
        Assert.Equal(SerializationSetupTestsRequestHandler.ReturnedResponse.Text, deserializedResponse.Text);
        Assert.Equal(SerializationSetupTestsRequestHandler.ReturnedResponse.Numbers, deserializedResponse.Numbers);
        Assert.Equal(SerializationSetupTestsRequestHandler.ReturnedResponse.Flags, deserializedResponse.Flags);
    }

    [Fact]
    public async Task SerializingResponse_WithDifferentSerializationConfiguration_DoesNotWriteTheContentCorrectly()
    {
        // Arrange
        var serializationConfig = new Action<JsonSerializerOptions>(options =>
        {
            options.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseUpper;
            options.IndentCharacter = '\t';
            options.WriteIndented = true;
        });
        await Initialize(options => { });

        var serializationOptions = new JsonSerializerOptions();
        serializationConfig(serializationOptions);

        // Act
        var client = _sut.GetTestClient();
        var result = await client.GetAsync(nameof(SerializationSetupTestsRequest));

        var responseContent = await result.Content.ReadAsStringAsync();
        var deserializedResponse = JsonSerializer.Deserialize<SerializationSetupTestsRequest.Response>(responseContent, serializationOptions);

        // Assert
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.NotNull(deserializedResponse);
        Assert.Equal(default, deserializedResponse.Id);
        Assert.Equal(default, deserializedResponse.Time);
        Assert.Equal(default, deserializedResponse.Text);
        Assert.Equal(default, deserializedResponse.Numbers);
        Assert.Equal(default, deserializedResponse.Flags);
    }

    public async ValueTask DisposeAsync()
    {
        await _sut.DisposeAsync();
    }
}

internal class SerializationSetupTestsRequestHandler : IHandle<SerializationSetupTestsRequest, SerializationSetupTestsRequest.Response>
{
    public static SerializationSetupTestsRequest.Response ReturnedResponse { get; } = new(
        Guid.NewGuid(),
        DateTime.UtcNow,
        "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
        [1, 2, 3],
        [
            new SerializationSetupTestsRequest.Flag(Guid.NewGuid(), true),
            new SerializationSetupTestsRequest.Flag(Guid.NewGuid(), false),
            new SerializationSetupTestsRequest.Flag(Guid.NewGuid(), null)
        ]);

    public Task<HttpResult<SerializationSetupTestsRequest.Response>> Handle(SerializationSetupTestsRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(HttpResult<SerializationSetupTestsRequest.Response>.WithStatusCode(HttpStatusCode.OK, ReturnedResponse));
    }
}
