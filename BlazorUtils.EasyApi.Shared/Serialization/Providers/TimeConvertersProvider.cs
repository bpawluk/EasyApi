using BlazorUtils.EasyApi.Shared.Serialization.Converters.Time;
using System;

namespace BlazorUtils.EasyApi.Shared.Serialization.Providers;

internal class TimeConvertersProvider : IConvertersProvider
{
    public IParamConverter<T>? Get<T>() => typeof(T) switch
    {
        Type type when type == typeof(DateOnly) => DateOnlyParamConverter.Instance as IParamConverter<T>,
        Type type when type == typeof(DateTimeOffset) => DateTimeOffsetParamConverter.Instance as IParamConverter<T>,
        Type type when type == typeof(DateTime) => DateTimeParamConverter.Instance as IParamConverter<T>,
        Type type when type == typeof(TimeOnly) => TimeOnlyParamConverter.Instance as IParamConverter<T>,
        Type type when type == typeof(TimeSpan) => TimeSpanParamConverter.Instance as IParamConverter<T>,
        _ => null
    };
}
