using BlazorUtils.EasyApi.Shared.Json;
using System.Text.Json;

namespace BlazorUtils.EasyApi.Shared.Serialization.Converters;

internal class DefaultParamConverter<T> : IParamConverter<T>
{
    private static DefaultParamConverter<T>? _instance;

    public static DefaultParamConverter<T> Instance => _instance ??= new DefaultParamConverter<T>();

    public T Read(string value) => JsonSerializer.Deserialize<T>(value, JsonOptions.Get)!;

    public string Write(T value) => JsonSerializer.Serialize(value, JsonOptions.Get);
}
