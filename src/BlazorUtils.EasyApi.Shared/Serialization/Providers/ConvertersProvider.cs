namespace BlazorUtils.EasyApi.Shared.Serialization.Providers;

internal class ConvertersProvider : IConvertersProvider
{
    private readonly IConvertersProvider[] _providers;

    public ConvertersProvider()
    {
        _providers =
        [
            new NullableConvertersProvider(this),
            new CustomConvertersProvider(),
            new SystemConvertersProvider(),
            new TimeConvertersProvider(),
            new EnumConvertersProvider(),
            new DefaultConvertersProvider()
        ];
    }

    public IParamConverter<T>? Get<T>()
    {
        foreach (var provider in _providers)
        {
            if (provider.Get<T>() is IParamConverter<T> converter)
            {
                return converter;
            }
        }
        return null;
    }
}
