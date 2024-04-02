using BlazorUtils.EasyApi.Shared.Serialization;
using System.Reflection;

namespace BlazorUtils.EasyApi.Shared.Contract;

public interface IPropertyWrapper
{
    string Name { get; }

    string? ReadFrom(IRequest request);

    void WriteTo(IRequest request, string? value);
}

public class PropertyWrapper<T> : IPropertyWrapper
{
    private readonly IParamConverter<T> _converter;
    private readonly PropertyInfo _propertyInfo;

    public string Name => _propertyInfo.Name;

    public PropertyWrapper(PropertyInfo propertyInfo, IParamConverter<T> converter)
    {
        _propertyInfo = propertyInfo;
        _converter = converter;
    }

    public string? ReadFrom(IRequest request)
    {
        var value = _propertyInfo.GetValue(request);
        if (typeof(T) == typeof(string))
        {
            return value as string;
        }
        return value == null ? default : _converter.Write((T)value);
    }

    public void WriteTo(IRequest request, string? value)
    {
        if (typeof(T) == typeof(string))
        {
            _propertyInfo.SetValue(request, value);
        }
        else
        {
            var convertedValue = string.IsNullOrEmpty(value) ? default : _converter.Read(value!);
            _propertyInfo.SetValue(request, convertedValue);
        }
    }
}
