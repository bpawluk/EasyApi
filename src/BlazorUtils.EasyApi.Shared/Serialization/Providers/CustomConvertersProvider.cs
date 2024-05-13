namespace BlazorUtils.EasyApi.Shared.Serialization.Providers;

// TODO: Configuring customized params serialization
internal class CustomConvertersProvider : IConvertersProvider
{
    public IParamConverter<T>? Get<T>() => null;
}
