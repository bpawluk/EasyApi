using System;

namespace BlazorUtils.EasyApi.Shared.Serialization.Converters.System;

internal class GuidParamConverter : IParamConverter<Guid>
{
    private const string _format = "D";
    private static GuidParamConverter? _instance;

    public static GuidParamConverter Instance => _instance ??= new GuidParamConverter();

    public Guid Read(string value) => Guid.ParseExact(value, _format);

    public string Write(Guid value) => value.ToString(_format);
}
