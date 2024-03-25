using BlazorUtils.EasyApi.Shared.Serialization.Converters.Other;

namespace BlazorUtils.EasyApi.Shared.Serialization.Providers;

internal class EnumConvertersProvider : IConvertersProvider
{
    public IParamConverter<T>? Get<T>() => typeof(T).IsEnum ? EnumParamConverter<T>.Instance : null;
}
