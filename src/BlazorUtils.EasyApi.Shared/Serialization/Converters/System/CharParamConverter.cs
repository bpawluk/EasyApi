using System;
using System.Linq;

namespace BlazorUtils.EasyApi.Shared.Serialization.Converters.System;

internal class CharParamConverter : IParamConverter<char>
{
    private static readonly Lazy<CharParamConverter> _instance = new(() => new CharParamConverter());

    public static CharParamConverter Instance => _instance.Value;

    private CharParamConverter() { }

    public char Read(string value) => value.Single();

    public string Write(char value) => value.ToString();
}
