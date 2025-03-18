using System;

namespace BlazorUtils.EasyApi.Shared.Serialization.Converters.Time;

internal class TimeSpanParamConverter : IParamConverter<TimeSpan>
{
    private static readonly Lazy<TimeSpanParamConverter> _instance = new(() => new TimeSpanParamConverter());

    public static TimeSpanParamConverter Instance => _instance.Value;

    private TimeSpanParamConverter() { }

    public TimeSpan Read(string value) => TimeSpan.Parse(value);

    public string Write(TimeSpan value) => value.ToString();
}
