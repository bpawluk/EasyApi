using System;
using System.Globalization;

namespace BlazorUtils.EasyApi.Shared.Serialization.Converters.Time;

internal class DateTimeOffsetParamConverter : IParamConverter<DateTimeOffset>
{
    private const string _format = "o";
    private static DateTimeOffsetParamConverter? _instance;

    public static DateTimeOffsetParamConverter Instance => _instance ??= new DateTimeOffsetParamConverter();

    public DateTimeOffset Read(string value) => DateTimeOffset.ParseExact(value, _format, CultureInfo.InvariantCulture);

    public string Write(DateTimeOffset value) => value.ToString(_format, CultureInfo.InvariantCulture);
}
