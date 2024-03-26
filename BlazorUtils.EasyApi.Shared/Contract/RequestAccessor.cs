using BlazorUtils.EasyApi.Shared.Reflection;
using BlazorUtils.EasyApi.Shared.Serialization;
using BlazorUtils.EasyApi.Shared.Serialization.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BlazorUtils.EasyApi.Shared.Contract;

public abstract class RequestAccessor
{
    public Type RequestType { get; }

    public Type? ResponseType { get; }

    public string Route { get; }

    public RequestAccessor(Type requestType, Type? responseType, string route)
    {
        RequestType = requestType;
        ResponseType = responseType;
        Route = route;
    }
}

public class RequestAccessor<Request> : RequestAccessor
    where Request : class, IRequest, new()
{
    public RequestAccessor() : base(
        GetRequestType(), 
        GetResponseType(),
        GetRoute()) { }

    public IEnumerable<IPropertyWrapper> GetBodyParams(Request request) => GetParamsWith<BodyParamAttribute>();

    public IEnumerable<IPropertyWrapper> GetHeaderParams(Request request) => GetParamsWith<HeaderParamAttribute>();

    public IEnumerable<IPropertyWrapper> GetQueryStringParams(Request request) => GetParamsWith<QueryStringParamAttribute>();

    public IEnumerable<IPropertyWrapper> GetRouteParams(Request request) => GetParamsWith<RouteParamAttribute>();

    private IEnumerable<IPropertyWrapper> GetParamsWith<AttributeType>() => typeof(Request)
        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
        .Where(property => property.GetCustomAttribute(typeof(AttributeType)) is AttributeType)
        .Select(property => GetPropertyWrapper(property));

    private IPropertyWrapper GetPropertyWrapper(PropertyInfo property)
    {
        var propertyType = property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
            ? Nullable.GetUnderlyingType(property.PropertyType)!
            : property.PropertyType;
        return (typeof(PropertyWrapper<>)
            .Apply(propertyType)
            .Create(property, GetConverter(propertyType)) as IPropertyWrapper)!;
    }

    private IParamConverter GetConverter(Type type) => (new ConvertersProvider().InvokeGeneric(nameof(IConvertersProvider.Get), type) as IParamConverter)!;

    private static Type GetRequestType() => typeof(Request);

    private static Type? GetResponseType() => GetRequestType()
        .GetInterfaces()
        .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>))?
        .GetGenericArguments()
        .Single();

    private static string GetRoute()
    {
        if (Attribute.GetCustomAttribute(GetRequestType(), typeof(RouteAttribute)) is RouteAttribute route)
        {
            return route.Value;
        }
        return string.Empty;
    }
}
