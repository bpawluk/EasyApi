using BlazorUtils.EasyApi.Shared.Json;
using BlazorUtils.EasyApi.Shared.Serialization.Converters;

namespace BlazorUtils.EasyApi.Shared.Serialization.Providers;

internal class DefaultConvertersProvider(JsonOptionsProvider jsonOptions) : IConvertersProvider
{
    private readonly JsonOptionsProvider _jsonOptions = jsonOptions;

    public IParamConverter<T>? Get<T>() => DefaultParamConverter<T>.GetInstance(_jsonOptions);
}
