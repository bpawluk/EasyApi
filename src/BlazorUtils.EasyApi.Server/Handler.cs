using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;

namespace BlazorUtils.EasyApi.Server;

public abstract class Handler : IHandle
{
    internal HttpContext? HttpContext { get; set; }

    internal AuthenticationStateProvider? AuthStateProvider { get; set; }

    public async Task<ClaimsPrincipal> GetUser()
    {
        if (HttpContext is not null)
        {
            return HttpContext.User;
        }

        if (AuthStateProvider is not null)
        {
            var authenticationState = await AuthStateProvider.GetAuthenticationStateAsync();
            return authenticationState.User;
        }

        return new ClaimsPrincipal();
    }
}

public abstract class Handler<Request> : Handler, IHandle<Request>
    where Request : class, IRequest, new()
{
    public abstract Task<HttpResult> Handle(Request request, CancellationToken cancellationToken);
}

public abstract class Handler<Request, Response> : Handler, IHandle<Request, Response>
    where Request : class, IRequest<Response>, new()
{
    public abstract Task<HttpResult<Response>> Handle(Request request, CancellationToken cancellationToken);
}