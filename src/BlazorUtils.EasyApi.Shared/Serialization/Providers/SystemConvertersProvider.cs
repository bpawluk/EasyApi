using BlazorUtils.EasyApi.Shared.Serialization.Converters.System;
using System;

namespace BlazorUtils.EasyApi.Shared.Serialization.Providers;

internal class SystemConvertersProvider : IConvertersProvider
{
    public IParamConverter<T>? Get<T>() => typeof(T) switch
    {
        Type type when type == typeof(char) => CharParamConverter.Instance as IParamConverter<T>,
        Type type when type == typeof(Guid) => GuidParamConverter.Instance as IParamConverter<T>,
        Type type when type == typeof(string) => StringParamConverter.Instance as IParamConverter<T>,
        Type type when type == typeof(Uri) => UriParamConverter.Instance as IParamConverter<T>,
        _ => null
    };
}
