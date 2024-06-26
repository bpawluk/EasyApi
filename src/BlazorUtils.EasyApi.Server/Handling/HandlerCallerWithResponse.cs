﻿using System.Threading;
using System.Threading.Tasks;

namespace BlazorUtils.EasyApi.Server.Handling;

internal class HandlerCaller<Request, Response> : ICall<Request, Response>
    where Request : class, IRequest<Response>, new()
{
    private readonly IHandle<Request, Response> _handler;

    public HandlerCaller(IHandle<Request, Response> handler)
    {
        _handler = handler;
    }

    public Task<Response> Call(Request request) => Call(request, CancellationToken.None);

    public async Task<Response> Call(Request request, CancellationToken cancellationToken)
    {
        var result = await _handler.Handle(request, cancellationToken);
        result.EnsureSucceeded();
        return result.Response!;
    }

    public Task<HttpResult<Response>> CallHttp(Request request) => CallHttp(request, CancellationToken.None);

    public Task<HttpResult<Response>> CallHttp(Request request, CancellationToken cancellationToken) => _handler.Handle(request, cancellationToken);
}
