using System;

namespace BlazorUtils.EasyApi.Shared.Serialization.Converters.System;

internal class GuidParamConverter : IParamConverter<Guid>
{
    private const string _format = "D";

    private static readonly Lazy<GuidParamConverter> _instance = new(() => new GuidParamConverter());

    public static GuidParamConverter Instance => _instance.Value;

    private GuidParamConverter() { }

    public Guid Read(string value) => Guid.ParseExact(value, _format);

    public string Write(Guid value) => value.ToString(_format);
}
