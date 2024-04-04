using System;
using System.Globalization;

namespace BlazorUtils.EasyApi.Shared.Serialization.Converters.Time;

internal class DateOnlyParamConverter : IParamConverter<DateOnly>
{
    private const string _format = "o";
    private static DateOnlyParamConverter? _instance;

    public static DateOnlyParamConverter Instance => _instance ??= new DateOnlyParamConverter();

    public DateOnly Read(string value) => DateOnly.ParseExact(value, _format, CultureInfo.InvariantCulture);

    public string Write(DateOnly value) => value.ToString(_format, CultureInfo.InvariantCulture);
}
