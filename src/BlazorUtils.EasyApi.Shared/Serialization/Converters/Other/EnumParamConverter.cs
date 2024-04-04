using System;

namespace BlazorUtils.EasyApi.Shared.Serialization.Converters.Other;

internal class EnumParamConverter<T> : IParamConverter<T>
{
    private static EnumParamConverter<T>? _instance;

    public static EnumParamConverter<T> Instance => _instance ??= new EnumParamConverter<T>();

    public T Read(string value) => (T)Enum.Parse(typeof(T)!, value);

    public string Write(T value) => value!.ToString()!;
}
