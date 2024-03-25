using System;

namespace BlazorUtils.EasyApi.Shared.Reflection;

public static class GenericsUtils
{
    public static Type Apply(this Type genericType, params Type[] genericArguments) => genericType.MakeGenericType(genericArguments);
}
