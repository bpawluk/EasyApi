using System;
using System.Reflection;

namespace BlazorUtils.EasyApi.Shared.Reflection;

internal static class InstanceUtils
{
    private const BindingFlags _instanceMethod = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
    private const BindingFlags _staticMethod = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

    public static object Create(this Type type, params object?[] constructorArguments) => Activator.CreateInstance(type, constructorArguments)!;

    public static object Invoke(this Type type, string methodName, params object?[] methodArguments)
    {
        MethodInfo method = type.GetMethod(methodName, _staticMethod)!;
        return method.Invoke(null, methodArguments)!;
    }

    public static object Invoke(this object instance, string methodName, params object?[] methodArguments)
    {
        MethodInfo method = instance.GetType().GetMethod(methodName, _instanceMethod)!;
        return method.Invoke(instance, methodArguments)!;
    }

    public static object InvokeGeneric(this Type type, string methodName, Type[] genericArguments, params object?[] methodArguments)
    {
        MethodInfo method = type.GetMethod(methodName, _staticMethod)!.MakeGenericMethod(genericArguments);
        return method.Invoke(null, methodArguments)!;
    }

    public static object InvokeGeneric(this Type type, string methodName, Type genericArgument, params object?[] methodArguments) =>
        type.InvokeGeneric(methodName, new Type[] { genericArgument }, methodArguments);

    public static object InvokeGeneric(this object instance, string methodName, Type[] genericArguments, params object?[] methodArguments)
    {
        MethodInfo method = instance.GetType().GetMethod(methodName, _instanceMethod)!.MakeGenericMethod(genericArguments);
        return method.Invoke(instance, methodArguments)!;
    }

    public static object InvokeGeneric(this object instance, string methodName, Type genericArgument, params object?[] methodArguments) =>
        instance.InvokeGeneric(methodName, new Type[] { genericArgument }, methodArguments);
}
