using System.Reflection;

namespace BlazorUtils.EasyApi.Tests.SUT.Contract;

internal class HandlerBase
{
    protected static Task<HttpResult> HandleRequest(IRequest request) 
        => Task.FromResult(IsFilled(request) ? HttpResult.Ok() : HttpResult.BadRequest());

    private static bool IsFilled(object value)
    {
        if (IsNullOrDefault(value))
        {
            return false;
        }

        var valueType = value.GetType();
        if (valueType.Namespace?.StartsWith("System") is false)
        {
            foreach (var property in valueType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!IsFilled(property.GetValue(value)!))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static bool IsNullOrDefault(object value)
    {
        var type = value.GetType();
        if (type.IsValueType)
        {
            return Equals(value, Activator.CreateInstance(type));
        }
        return value is null;
    }
}