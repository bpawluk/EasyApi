using System;
using System.Globalization;

namespace BlazorUtils.EasyApi.Shared.Serialization.Converters.Time;

internal class TimeOnlyParamConverter : IParamConverter<TimeOnly>
{
    private const string _format = "o";
    private static TimeOnlyParamConverter? _instance;

    public static TimeOnlyParamConverter Instance => _instance ??= new TimeOnlyParamConverter();

    public TimeOnly Read(string value) => TimeOnly.ParseExact(value, _format, CultureInfo.InvariantCulture);

    public string Write(TimeOnly value) => value.ToString(_format, CultureInfo.InvariantCulture);
}
