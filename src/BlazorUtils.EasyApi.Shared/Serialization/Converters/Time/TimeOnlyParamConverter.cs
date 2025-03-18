using System;
using System.Globalization;

namespace BlazorUtils.EasyApi.Shared.Serialization.Converters.Time;

internal class TimeOnlyParamConverter : IParamConverter<TimeOnly>
{
    private const string _format = "o";

    private static readonly Lazy<TimeOnlyParamConverter> _instance = new(() => new TimeOnlyParamConverter());

    public static TimeOnlyParamConverter Instance => _instance.Value;

    private TimeOnlyParamConverter() { }

    public TimeOnly Read(string value) => TimeOnly.ParseExact(value, _format, CultureInfo.InvariantCulture);

    public string Write(TimeOnly value) => value.ToString(_format, CultureInfo.InvariantCulture);
}
