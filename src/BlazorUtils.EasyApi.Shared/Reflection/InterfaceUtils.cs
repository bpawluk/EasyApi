using System;
using System.Linq;

namespace BlazorUtils.EasyApi.Shared.Reflection;

internal static class InterfaceUtils
{
    public static bool Implements<InterfaceType>(this Type type) => type.Implements(typeof(InterfaceType));

    public static bool Implements(this Type type, Type interfaceType) => !type.IsInterface && type
        .GetInterfaces()
        .Any(iface => iface == interfaceType);
}
