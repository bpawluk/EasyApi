using System;

namespace BlazorUtils.EasyApi.Shared.Reflection;

public static class RequestUtils
{
    public static string GetRoute<Request>()
        where Request : IRequest
    {
        var routeAttribute = Attribute.GetCustomAttribute(typeof(Request), typeof(RouteAttribute)) as RouteAttribute;
        return routeAttribute?.Value ?? string.Empty;
    }
}
