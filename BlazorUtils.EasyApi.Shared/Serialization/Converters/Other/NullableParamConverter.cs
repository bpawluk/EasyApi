using System;

namespace BlazorUtils.EasyApi.Shared.Serialization.Converters.Other;

internal class NullableParamConverter<T> : IParamConverter<Nullable<T>> where T : struct
{
    private static NullableParamConverter<T>? _instance;

    private readonly IParamConverter<T> _wrappedValueConverter;

    public NullableParamConverter(IConvertersProvider provider)
    {
        _wrappedValueConverter = provider.Get<T>()!;
    }

    public static NullableParamConverter<T> GetInstance(IConvertersProvider provider) => _instance ??= new NullableParamConverter<T>(provider);

    public Nullable<T> Read(string value) => _wrappedValueConverter.Read(value);

    public string Write(Nullable<T> value) => _wrappedValueConverter.Write(value!.Value);
}
