using System;
using System.Linq;

namespace BlazorUtils.EasyApi.Shared.Reflection;

public static class GenericsUtils
{
    public static Type Apply(this Type genericType, params Type[] genericArguments) => genericType.MakeGenericType(genericArguments);

    public static string GetGenericName(this Type genericType)
    {
        if (!genericType.IsGenericType)
        {
            return genericType.Name;
        }
        var typeName = genericType.Name.Split('`')[0];
        var argNames = genericType.GetGenericArguments().Select(arg => arg.Name);
        return $"{typeName}<{string.Join(", ", argNames)}>";
    }
}
