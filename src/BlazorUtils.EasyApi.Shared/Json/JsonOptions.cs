using System.Text.Json;

namespace BlazorUtils.EasyApi.Shared.Json;

public static class JsonOptions
{
    public static JsonSerializerOptions Get { get; } = Create();

    public static JsonSerializerOptions Create() => new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
}
