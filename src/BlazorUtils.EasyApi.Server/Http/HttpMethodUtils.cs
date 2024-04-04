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
            Type type when type.Implements<IGet>() => HttpMethods.Get,
            Type type when type.Implements<IHead>() => HttpMethods.Head,
            Type type when type.Implements<IPost>() => HttpMethods.Post,
            Type type when type.Implements<IPut>() => HttpMethods.Put,
            Type type when type.Implements<IPatch>() => HttpMethods.Patch,
            Type type when type.Implements<IDelete>() => HttpMethods.Delete,
            _ => throw new ArgumentException($"Failed to match HTTP method for {typeof(Request).Name}")
        };
    }

    public static string GetHttpMethod<Request, Response>()
        where Request : class, IRequest<Response>, new()
    {
        return typeof(Request) switch
        {
            Type type when type.Implements<IGet<Response>>() => HttpMethods.Get,
            Type type when type.Implements<IPost<Response>>() => HttpMethods.Post,
            Type type when type.Implements<IPut<Response>>() => HttpMethods.Put,
            Type type when type.Implements<IPatch<Response>>() => HttpMethods.Patch,
            Type type when type.Implements<IDelete<Response>>() => HttpMethods.Delete,
            _ => throw new ArgumentException($"Failed to match HTTP method for {typeof(Request).Name}")
        };
    }
}
