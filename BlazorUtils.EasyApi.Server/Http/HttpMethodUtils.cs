using BlazorUtils.EasyApi.Shared.Reflection;
using Microsoft.AspNetCore.Http;
using System;

namespace BlazorUtils.EasyApi.Server.Http;

public static class HttpMethodUtils
{
    public static string GetHttpMethod<Request>()
        where Request : class, IRequest, new()
    {
        return typeof(Request) switch
        {
            Type type when type.Implements(typeof(IGet)) => HttpMethods.Get,
            Type type when type.Implements(typeof(IHead)) => HttpMethods.Head,
            Type type when type.Implements(typeof(IPost)) => HttpMethods.Post,
            Type type when type.Implements(typeof(IPut)) => HttpMethods.Put,
            Type type when type.Implements(typeof(IPatch)) => HttpMethods.Patch,
            Type type when type.Implements(typeof(IDelete)) => HttpMethods.Delete,
            _ => throw new ArgumentException($"Failed to match HTTP method for {typeof(Request).Name}")
        };
    }

    public static string GetHttpMethod<Request, Response>()
        where Request : class, IRequest<Response>, new()
    {
        return typeof(Request) switch
        {
            Type type when type.Implements(typeof(IGet<Response>)) => HttpMethods.Get,
            Type type when type.Implements(typeof(IPost<Response>)) => HttpMethods.Post,
            Type type when type.Implements(typeof(IPut<Response>)) => HttpMethods.Post,
            Type type when type.Implements(typeof(IPatch<Response>)) => HttpMethods.Patch,
            Type type when type.Implements(typeof(IDelete<Response>)) => HttpMethods.Delete,
            _ => throw new ArgumentException($"Failed to match HTTP method for {typeof(Request).Name}")
        };
    }
}
