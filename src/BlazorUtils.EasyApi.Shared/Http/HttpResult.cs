using BlazorUtils.EasyApi.Shared.Http;
using System.Net;

namespace BlazorUtils.EasyApi;

public class HttpResult(HttpStatusCode statusCode) : HttpResultBase(statusCode)
{
    protected override bool Succeeded => IsSuccessStatusCode;

    protected override string StatusPhrase => StatusCode.ToPhrase();

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
