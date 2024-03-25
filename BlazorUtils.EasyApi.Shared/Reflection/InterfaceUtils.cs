using System;
using System.Linq;

namespace BlazorUtils.EasyApi.Shared.Reflection;

public static class InterfaceUtils
{
    public static bool Implements(this Type type, Type interfaceType) => type
        .GetInterfaces()
        .Any(iface => iface == interfaceType);

    public static bool ImplementsGeneric(this Type type, Type interfaceType) => type
        .GetInterfaces()
        .Any(iface => iface.IsGenericType && iface.GetGenericTypeDefinition() == interfaceType);
}
