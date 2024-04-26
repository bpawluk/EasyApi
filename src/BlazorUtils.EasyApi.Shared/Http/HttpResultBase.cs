using System.Net;
using System.Net.Http;

namespace BlazorUtils.EasyApi;

public abstract class HttpResultBase
{
    protected abstract bool Succeeded { get; }

    protected abstract string StatusPhrase { get; }

    protected bool IsSuccessStatusCode => (int)StatusCode >= 200 && (int)StatusCode <= 299;

    public HttpStatusCode StatusCode { get; }

    protected HttpResultBase(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }

    public void EnsureSucceeded()
    {
        if (!Succeeded)
        {
            throw new HttpRequestException(StatusPhrase);
        }
    }
}
