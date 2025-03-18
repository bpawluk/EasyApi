using System;

namespace BlazorUtils.EasyApi.Shared.Serialization.Converters.System;

internal class StringParamConverter : IParamConverter<string>
{
    private static readonly Lazy<StringParamConverter> _instance = new(() => new StringParamConverter());

    public static StringParamConverter Instance => _instance.Value;

    private StringParamConverter() { }

    public string Read(string value) => value;

    public string Write(string value) => value;
}
