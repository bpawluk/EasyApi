using System;
using System.Net.Http;

namespace BlazorUtils.EasyApi.Client.Http;

internal static class HttpMethodUtils
{
    public static HttpMethod GetHttpMethod<Request>(this Request request)
        where Request : class, IRequest, new()
    {
        return request switch
        {
            IGet => HttpMethod.Get,
            IHead => HttpMethod.Head,
            IPost => HttpMethod.Post,
            IPut => HttpMethod.Put,
            IPatch => HttpMethod.Patch,
            IDelete => HttpMethod.Delete,
            _ => throw new ArgumentException($"Failed to match HTTP method for {request.GetType().Name}")
        };
    }

    public static HttpMethod GetHttpMethod<Request, Response>(this Request request)
        where Request : class, IRequest<Response>, new()
    {
        return request switch
        {
            IGet<Response> => HttpMethod.Get,
            IPost<Response> => HttpMethod.Post,
            IPut<Response> => HttpMethod.Put,
            IPatch<Response> => HttpMethod.Patch,
            IDelete<Response> => HttpMethod.Delete,
            _ => throw new ArgumentException($"Failed to match HTTP method for {request.GetType().Name}")
        };
    }

    public static bool CanHaveContent(this HttpMethod method) => method != HttpMethod.Get && method != HttpMethod.Head;
}
