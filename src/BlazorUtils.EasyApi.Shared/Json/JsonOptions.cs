using System.Text.Json;

namespace BlazorUtils.EasyApi.Shared.Json;

internal static class JsonOptions
{
    public static JsonSerializerOptions Get { get; } = Create();

    public static JsonSerializerOptions Create() => new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
}
