using System.Threading;

namespace BlazorUtils.EasyApi.Shared.Serialization.Converters.Other;

internal class NullableParamConverter<T> : IParamConverter<T?> where T : struct
{
    private readonly IParamConverter<T> _wrappedValueConverter;

    private static readonly Lock _lock = new();
    private static NullableParamConverter<T>? _instance;

    public static NullableParamConverter<T> GetInstance(IConvertersProvider provider)
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                _instance ??= new NullableParamConverter<T>(provider);
            }
        }
        return _instance;
    }

    private NullableParamConverter(IConvertersProvider provider) 
    {
        _wrappedValueConverter = provider.Get<T>()!;
    }

    public T? Read(string value) => _wrappedValueConverter.Read(value);

    public string Write(T? value) => _wrappedValueConverter.Write(value!.Value);
}
