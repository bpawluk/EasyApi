using BlazorUtils.EasyApi.Shared.Serialization.Converters.System;
using System;

namespace BlazorUtils.EasyApi.Shared.Serialization.Converters.Time;

internal class TimeSpanParamConverter : IParamConverter<TimeSpan>
{
    private static TimeSpanParamConverter? _instance;

    public static TimeSpanParamConverter Instance => _instance ??= new TimeSpanParamConverter();

    public TimeSpan Read(string value) => TimeSpan.Parse(value);

    public string Write(TimeSpan value) => value.ToString();
}
