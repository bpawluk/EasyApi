using BlazorUtils.EasyApi.Shared.Rendering;
using Microsoft.AspNetCore.Http;

namespace BlazorUtils.EasyApi.Server.Rendering;

internal class InteractivityDetector(IHttpContextAccessor accessor) : IInteractivityDetector
{
    private readonly IHttpContextAccessor _accessor = accessor;

    public bool IsInteractiveWebAssembly => false; 

    public bool IsInteractiveServer => _accessor.HttpContext?.WebSockets.IsWebSocketRequest != false;
}
