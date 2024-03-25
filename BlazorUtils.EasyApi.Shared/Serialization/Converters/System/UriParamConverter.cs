using System;

namespace BlazorUtils.EasyApi.Shared.Serialization.Converters.System;

internal class UriParamConverter : IParamConverter<Uri>
{
    private static UriParamConverter? _instance;

    public static UriParamConverter Instance => _instance ??= new UriParamConverter();

    public Uri Read(string value) => new(value);

    public string Write(Uri value) => value.ToString();
}
