using System.Net;
using System.Reflection;

namespace BlazorUtils.EasyApi.Tests.SUT.Contract;

internal class HandlerBase
{
    protected static Task<HttpResult> HandleRequest(IRequest request, HttpStatusCode successStatusCode = HttpStatusCode.OK)
        => Task.FromResult(IsFilled(request) 
            ? HttpResult.WithStatusCode(successStatusCode) 
            : HttpResult.BadRequest());

    protected static Task<HttpResult<Response>> HandleRequest<Response>(IRequest<Response> request, HttpStatusCode successStatusCode = HttpStatusCode.OK)
        => Task.FromResult(IsFilled(request) 
            ? HttpResult<Response>.WithStatusCode(successStatusCode, GetResponse(request)) 
            : HttpResult<Response>.BadRequest());

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

    private static Response GetResponse<Response>(IRequest<Response> request)
    {
        var response = Activator.CreateInstance(typeof(Response))!;
        foreach (var requestProperty in request.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var responseProperty = typeof(Response).GetProperty(requestProperty.Name);
            if (responseProperty?.CanWrite is true && responseProperty.PropertyType.IsAssignableFrom(requestProperty.PropertyType))
            {
                responseProperty.SetValue(response, requestProperty.GetValue(request));
            }
        }
        return (Response)response;
    }
}