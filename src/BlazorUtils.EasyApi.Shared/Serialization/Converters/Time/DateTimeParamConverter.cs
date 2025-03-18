using System;
using System.Globalization;

namespace BlazorUtils.EasyApi.Shared.Serialization.Converters.Time;

internal class DateTimeParamConverter : IParamConverter<DateTime>
{
    private const string _format = "o";

    private static readonly Lazy<DateTimeParamConverter> _instance = new(() => new DateTimeParamConverter());

    public static DateTimeParamConverter Instance => _instance.Value;

    private DateTimeParamConverter() { }

    public DateTime Read(string value) => DateTime.ParseExact(value, _format, CultureInfo.InvariantCulture);

    public string Write(DateTime value) => value.ToUniversalTime().ToString(_format, CultureInfo.InvariantCulture);
}
