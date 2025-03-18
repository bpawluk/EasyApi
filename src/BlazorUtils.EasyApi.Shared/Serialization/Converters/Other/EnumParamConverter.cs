using System;

namespace BlazorUtils.EasyApi.Shared.Serialization.Converters.Other;

internal class EnumParamConverter<T> : IParamConverter<T>
{
    private static readonly Lazy<EnumParamConverter<T>> _instance = new(() => new EnumParamConverter<T>());

    public static EnumParamConverter<T> Instance => _instance.Value;

    private EnumParamConverter() { }

    public T Read(string value) => (T)Enum.Parse(typeof(T)!, value);

    public string Write(T value) => value!.ToString()!;
}
