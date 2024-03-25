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
        var paramValue = _propertyInfo.GetValue(request);
        return paramValue == null ? default : _converter.Write((T)paramValue);
    }

    public void WriteTo(IRequest request, string? value) 
    {
        var paramValue = value == null ? default : _converter.Read(value!);
        _propertyInfo.SetValue(request, paramValue);
    }
}
