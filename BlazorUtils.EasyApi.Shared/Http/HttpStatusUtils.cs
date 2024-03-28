using System.Net;

namespace BlazorUtils.EasyApi.Shared.Http;

internal static class HttpStatusUtils
{
    public static string ToPhrase(this HttpStatusCode code) => $"[{(int)code} {code}]";
}
