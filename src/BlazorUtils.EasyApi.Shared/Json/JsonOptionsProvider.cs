using System;
using System.Text.Json;

namespace BlazorUtils.EasyApi.Shared.Json;

internal class JsonOptionsProvider
{
    private readonly Lazy<JsonSerializerOptions> _instance;
    private Action<JsonSerializerOptions>? _configuration;

    public JsonOptionsProvider()
    {
        _instance = new(Create);
        _configuration = null;
    }

    public JsonSerializerOptions Create()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        _configuration?.Invoke(options);
        return options;
    }

    public JsonSerializerOptions Get() => _instance.Value;

    public void Configure(Action<JsonSerializerOptions> configuration) => _configuration = configuration;
}
