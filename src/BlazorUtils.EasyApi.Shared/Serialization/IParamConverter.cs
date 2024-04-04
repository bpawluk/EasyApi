namespace BlazorUtils.EasyApi.Shared.Serialization;

public interface IParamConverter { }

public interface IParamConverter<T> : IParamConverter
{
    T Read(string value);

    string Write(T value);
}
