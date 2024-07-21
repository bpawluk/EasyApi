using System.Net;
using System.Net.Http;

namespace BlazorUtils.EasyApi;

public abstract class HttpResultBase(HttpStatusCode statusCode)
{
    protected abstract bool Succeeded { get; }

    protected abstract string StatusPhrase { get; }

    public HttpStatusCode StatusCode { get; } = statusCode;

    public bool IsSuccessStatusCode => (int)StatusCode >= 200 && (int)StatusCode <= 299;

    public void EnsureSucceeded()
    {
        if (!Succeeded)
        {
            throw new HttpRequestException(StatusPhrase);
        }
    }
}
