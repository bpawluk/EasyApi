using BlazorUtils.EasyApi.Shared.Serialization.Converters;

namespace BlazorUtils.EasyApi.Shared.Serialization.Providers;

internal class DefaultConvertersProvider : IConvertersProvider
{
    public IParamConverter<T>? Get<T>() => DefaultParamConverter<T>.Instance;
}
