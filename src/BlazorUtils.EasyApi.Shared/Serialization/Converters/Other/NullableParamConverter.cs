namespace BlazorUtils.EasyApi.Shared.Serialization.Converters.Other;

internal class NullableParamConverter<T>(IConvertersProvider provider) 
    : IParamConverter<T?> where T : struct
{
    private static NullableParamConverter<T>? _instance;

    private readonly IParamConverter<T> _wrappedValueConverter = provider.Get<T>()!;

    public static NullableParamConverter<T> GetInstance(IConvertersProvider provider) => _instance ??= new NullableParamConverter<T>(provider);

    public T? Read(string value) => _wrappedValueConverter.Read(value);

    public string Write(T? value) => _wrappedValueConverter.Write(value!.Value);
}
