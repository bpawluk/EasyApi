using BlazorUtils.EasyApi.Shared.Prerendering;
using Microsoft.AspNetCore.Http;

namespace BlazorUtils.EasyApi.Server.Prerendering;

internal class PrerenderingDetector(IHttpContextAccessor accessor) : IPrerenderingDetector
{
    private readonly IHttpContextAccessor _accessor = accessor;

    public bool IsPrerendering => _accessor.HttpContext?.WebSockets.IsWebSocketRequest == false;
}
