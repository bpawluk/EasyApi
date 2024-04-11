using BlazorUtils.EasyApi.Shared.Http;
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

public class HttpResult : HttpResultBase
{
    protected override bool Succeeded => IsSuccessStatusCode;

    protected override string StatusPhrase => StatusCode.ToPhrase();

    private HttpResult(HttpStatusCode statusCode) : base(statusCode) { }

    public static HttpResult Ok() => new(HttpStatusCode.OK);

    public static HttpResult Created() => new(HttpStatusCode.Created);

    public static HttpResult Accepted() => new(HttpStatusCode.Accepted);

    public static HttpResult NoContent() => new(HttpStatusCode.NoContent);

    public static HttpResult BadRequest() => new(HttpStatusCode.BadRequest);

    public static HttpResult Unauthorized() => new(HttpStatusCode.Unauthorized);

    public static HttpResult Forbidden() => new(HttpStatusCode.Forbidden);

    public static HttpResult NotFound() => new(HttpStatusCode.NotFound);

    public static HttpResult Conflict() => new(HttpStatusCode.Conflict);

    public static HttpResult UnprocessableEntity() => new(HttpStatusCode.UnprocessableEntity);

    public static HttpResult WithStatusCode(HttpStatusCode statusCode) => new(statusCode);
}

public class HttpResult<ResponseType> : HttpResultBase
{
    protected override bool Succeeded => IsSuccessStatusCode && HasResponse;

    protected override string StatusPhrase => $"{StatusCode.ToPhrase()}{(IsSuccessStatusCode && !HasResponse ? " with missing Response" : string.Empty)}";

    public bool HasResponse { get; }

    public ResponseType? Response { get; }

    private HttpResult(HttpStatusCode statusCode) : base(statusCode) { }

    private HttpResult(HttpStatusCode statusCode, ResponseType response) : base(statusCode)
    {
        HasResponse = response != null;
        Response = response;
    }

    public static HttpResult<ResponseType> Ok(ResponseType response) => new(HttpStatusCode.OK, response);

    public static HttpResult<ResponseType> Created(ResponseType response) => new(HttpStatusCode.Created, response);

    public static HttpResult<ResponseType> Accepted(ResponseType response) => new(HttpStatusCode.Accepted, response);

    public static HttpResult<ResponseType> BadRequest() => new(HttpStatusCode.BadRequest);
    public static HttpResult<ResponseType> BadRequest(ResponseType response) => new(HttpStatusCode.BadRequest, response);

    public static HttpResult<ResponseType> Unauthorized() => new(HttpStatusCode.Unauthorized);
    public static HttpResult<ResponseType> Unauthorized(ResponseType response) => new(HttpStatusCode.Unauthorized, response);

    public static HttpResult<ResponseType> Forbidden() => new(HttpStatusCode.Forbidden);
    public static HttpResult<ResponseType> Forbidden(ResponseType response) => new(HttpStatusCode.Forbidden, response);

    public static HttpResult<ResponseType> NotFound() => new(HttpStatusCode.NotFound);
    public static HttpResult<ResponseType> NotFound(ResponseType response) => new(HttpStatusCode.NotFound, response);

    public static HttpResult<ResponseType> Conflict() => new(HttpStatusCode.Conflict);
    public static HttpResult<ResponseType> Conflict(ResponseType response) => new(HttpStatusCode.Conflict, response);

    public static HttpResult<ResponseType> UnprocessableEntity() => new(HttpStatusCode.UnprocessableEntity);
    public static HttpResult<ResponseType> UnprocessableEntity(ResponseType response) => new(HttpStatusCode.UnprocessableEntity, response);

    public static HttpResult<ResponseType> WithStatusCode(HttpStatusCode statusCode) => new(statusCode);
    public static HttpResult<ResponseType> WithStatusCode(HttpStatusCode statusCode, ResponseType response) => new(statusCode, response);
}
