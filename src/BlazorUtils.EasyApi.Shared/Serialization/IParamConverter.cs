namespace BlazorUtils.EasyApi.Shared.Serialization;

internal interface IParamConverter { }

internal interface IParamConverter<T> : IParamConverter
{
    T Read(string value);

    string Write(T value);
}
