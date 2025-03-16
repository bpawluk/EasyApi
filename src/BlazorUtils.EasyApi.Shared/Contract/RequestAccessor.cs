using BlazorUtils.EasyApi.Shared.Reflection;
using BlazorUtils.EasyApi.Shared.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BlazorUtils.EasyApi.Shared.Contract;

internal abstract class RequestAccessor
{
    public Type RequestType { get; init; } = default!;

    public Type? ResponseType { get; init; } = default!;

    public RouteInfo RouteInfo { get; init; } = default!;

    public IEnumerable<PropertyWrapper> BodyParams { get; init; } = default!;

    public IEnumerable<PropertyWrapper> HeaderParams { get; init; } = default!;

    public IEnumerable<PropertyWrapper> QueryStringParams { get; init; } = default!;

    public IEnumerable<PropertyWrapper> RouteParams { get; init; } = default!;
}

internal class RequestAccessor<Request> : RequestAccessor
    where Request : class, IRequest, new()
{
    public RequestAccessor(IConvertersProvider convertersProvider) : base()
    {
        RequestType = GetRequestType();
        ResponseType = GetResponseType();
        RouteInfo = GetRouteInfo();
        BodyParams = GetParamsWith<BodyParamAttribute>(convertersProvider);
        HeaderParams = GetParamsWith<HeaderParamAttribute>(convertersProvider);
        QueryStringParams = GetParamsWith<QueryStringParamAttribute>(convertersProvider);
        RouteParams = GetParamsWith<RouteParamAttribute>(convertersProvider);
    }

    private static Type GetRequestType() => typeof(Request);

    private static Type? GetResponseType() => GetRequestType()
        .GetInterfaces()
        .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>))?
        .GetGenericArguments()
        .Single();

    private static RouteInfo GetRouteInfo() => Attribute.GetCustomAttribute(GetRequestType(), typeof(RouteAttribute)) switch
    {
        ProtectedRouteAttribute protectedRoute => new RouteInfo(protectedRoute),
        RouteAttribute route => new RouteInfo(route),
        _ => RouteInfo.Default
    };

    private static PropertyWrapper[] GetParamsWith<AttributeType>(IConvertersProvider convertersProvider) => typeof(Request)
        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
        .Where(property => property.GetCustomAttribute(typeof(AttributeType)) is AttributeType)
        .Select(property => GetPropertyWrapper(convertersProvider, property))
        .ToArray();

    private static PropertyWrapper GetPropertyWrapper(IConvertersProvider convertersProvider, PropertyInfo property) => (typeof(PropertyWrapper<,>)
        .Apply(property.DeclaringType!, property.PropertyType)
        .Create(property, GetConverter(convertersProvider, property.PropertyType)) as PropertyWrapper)!;

    private static IParamConverter GetConverter(IConvertersProvider convertersProvider, Type type) => (convertersProvider.InvokeGeneric(nameof(IConvertersProvider.Get), type) as IParamConverter)!;
}
