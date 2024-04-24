using System;
using System.Linq.Expressions;
using System.Reflection;

namespace BlazorUtils.EasyApi.Shared.Reflection;

internal static class FastInvoke
{
    public static Func<DeclaringType, PropertyType> BuildGetter<DeclaringType, PropertyType>(PropertyInfo propertyInfo)
    {
        var instanceParam = Expression.Parameter(propertyInfo.DeclaringType!, "t");
        var propertyAccess = Expression.MakeMemberAccess(instanceParam, propertyInfo);
        var lambda = Expression.Lambda<Func<DeclaringType, PropertyType>>(propertyAccess, instanceParam);
        return lambda.Compile();
    }

    public static Action<DeclaringType, PropertyType> BuildSetter<DeclaringType, PropertyType>(PropertyInfo propertyInfo)
    {
        var instanceParam = Expression.Parameter(propertyInfo.DeclaringType!, "t");
        var propertyAccess = Expression.MakeMemberAccess(instanceParam, propertyInfo);
        var valueToSetParam = Expression.Parameter(typeof(PropertyType), "p");
        var valueSetter = Expression.Assign(propertyAccess, valueToSetParam);
        var lambda = Expression.Lambda<Action<DeclaringType, PropertyType>>(valueSetter, instanceParam, valueToSetParam);
        return lambda.Compile();
    }
}
