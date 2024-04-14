using BlazorUtils.EasyApi.Shared.Serialization;
using System.Reflection;

namespace BlazorUtils.EasyApi.Shared.Contract;

internal abstract class PropertyWrapper
{
    public abstract string Name { get; }

    public abstract string? ReadFrom(IRequest request);

    public abstract void WriteTo(IRequest request, string? value);
}

internal class PropertyWrapper<T> : PropertyWrapper
{
    private readonly IParamConverter<T> _converter;
    private readonly PropertyInfo _propertyInfo;

    public override string Name => _propertyInfo.Name;

    public PropertyWrapper(PropertyInfo propertyInfo, IParamConverter<T> converter)
    {
        _propertyInfo = propertyInfo;
        _converter = converter;
    }

    public override string? ReadFrom(IRequest request)
    {
        var value = _propertyInfo.GetValue(request);
        if (typeof(T) == typeof(string))
        {
            return value as string;
        }
        return value == null ? default : _converter.Write((T)value);
    }

    public override void WriteTo(IRequest request, string? value)
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
