using BlazorUtils.EasyApi.Shared.Serialization.Converters.System;
using System;
using System.Globalization;

namespace BlazorUtils.EasyApi.Shared.Serialization.Converters.Time;

internal class DateTimeParamConverter : IParamConverter<DateTime>
{
    private const string _format = "o";
    private static DateTimeParamConverter? _instance;

    public static DateTimeParamConverter Instance => _instance ??= new DateTimeParamConverter();

    public DateTime Read(string value) => DateTime.ParseExact(value, _format, CultureInfo.InvariantCulture);

    public string Write(DateTime value) => value.ToUniversalTime().ToString(_format, CultureInfo.InvariantCulture);
}
