using System.Linq;

namespace BlazorUtils.EasyApi.Shared.Serialization.Converters.System;

internal class CharParamConverter : IParamConverter<char>
{
    private static CharParamConverter? _instance;

    public static CharParamConverter Instance => _instance ??= new CharParamConverter();

    public char Read(string value) => value.Single();

    public string Write(char value) => value.ToString();
}
