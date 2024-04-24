using BlazorUtils.EasyApi.Shared.Reflection;
using BlazorUtils.EasyApi.Shared.Serialization;
using System;
using System.Reflection;

namespace BlazorUtils.EasyApi.Shared.Contract;

internal abstract class PropertyWrapper
{
    public string Name { get; init; } = default!;

    public abstract string? ReadFrom(IRequest request);

    public abstract void WriteTo(IRequest request, string? value);
}

internal class PropertyWrapper<DeclaringType, PropertyType> : PropertyWrapper
    where DeclaringType : class, IRequest, new()
{
    private readonly IParamConverter<PropertyType> _converter;
    private readonly Func<DeclaringType, PropertyType> _get;
    private readonly Action<DeclaringType, PropertyType> _set;

    public PropertyWrapper(PropertyInfo propertyInfo, IParamConverter<PropertyType> converter)
    {
        Name = propertyInfo.Name;
        _converter = converter;
        _get = FastInvoke.BuildGetter<DeclaringType, PropertyType>(propertyInfo);
        _set = FastInvoke.BuildSetter<DeclaringType, PropertyType>(propertyInfo);
    }

    public override string? ReadFrom(IRequest request)
    {
        DeclaringType? convertedRequest = request as DeclaringType;
        var value = _get(convertedRequest!);

        if (typeof(PropertyType) == typeof(string))
        {
            return value as string;
        }

        return value == null ? default : _converter.Write(value);
    }

    public override void WriteTo(IRequest request, string? value)
    {
        DeclaringType? convertedRequest = request as DeclaringType;
        PropertyType? convertedValue;

        if (typeof(PropertyType) == typeof(string))
        {
            convertedValue = (PropertyType)(value as object)!;
        }
        else
        {
            convertedValue = string.IsNullOrEmpty(value) ? default : _converter.Read(value!);
        }

        _set(convertedRequest!, convertedValue!);
    }
}
