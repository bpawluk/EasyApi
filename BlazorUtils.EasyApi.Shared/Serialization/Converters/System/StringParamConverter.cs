namespace BlazorUtils.EasyApi.Shared.Serialization.Converters.System;

internal class StringParamConverter : IParamConverter<string>
{
    private static StringParamConverter? _instance;

    public static StringParamConverter Instance => _instance ??= new StringParamConverter();

    public string Read(string value) => value;

    public string Write(string value) => value;
}
