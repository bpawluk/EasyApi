using Microsoft.AspNetCore.Http;

namespace BlazorUtils.EasyApi.Server.Persistence;

internal class PrerenderingDetector(IHttpContextAccessor accessor)
{
    private readonly IHttpContextAccessor _accessor = accessor;

    public bool IsPrerendering => _accessor.HttpContext?.WebSockets.IsWebSocketRequest == false;
}
