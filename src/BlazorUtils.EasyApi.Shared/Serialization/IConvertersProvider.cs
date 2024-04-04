namespace BlazorUtils.EasyApi.Shared.Serialization;

internal interface IConvertersProvider
{
    IParamConverter<T>? Get<T>();
}
