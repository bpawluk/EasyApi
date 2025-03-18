using System;

namespace BlazorUtils.EasyApi.Shared.Serialization.Converters.System;

internal class UriParamConverter : IParamConverter<Uri>
{
    private static readonly Lazy<UriParamConverter> _instance = new(() => new UriParamConverter());

    public static UriParamConverter Instance => _instance.Value;

    private UriParamConverter() { }

    public Uri Read(string value) => new(value);

    public string Write(Uri value) => value.ToString();
}
