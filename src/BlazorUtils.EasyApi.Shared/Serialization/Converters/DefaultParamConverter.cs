using System.Text.Json;
using System.Threading;
using BlazorUtils.EasyApi.Shared.Json;

namespace BlazorUtils.EasyApi.Shared.Serialization.Converters;

internal class DefaultParamConverter<T> : IParamConverter<T>
{
    private readonly JsonOptionsProvider _jsonOptions;

    private static readonly Lock _lock = new();
    private static DefaultParamConverter<T>? _instance;

    public static DefaultParamConverter<T> GetInstance(JsonOptionsProvider jsonOptions)
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                _instance ??= new DefaultParamConverter<T>(jsonOptions);
            }
        }
        return _instance;
    }

    private DefaultParamConverter(JsonOptionsProvider jsonOptions)
    {
        _jsonOptions = jsonOptions;
    }

    public T Read(string value) => JsonSerializer.Deserialize<T>(value, _jsonOptions.Get())!;

    public string Write(T value) => JsonSerializer.Serialize(value, _jsonOptions.Get());
}
