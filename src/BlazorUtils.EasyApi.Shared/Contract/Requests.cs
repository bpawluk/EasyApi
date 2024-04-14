using BlazorUtils.EasyApi.Shared.Exceptions;
using System;
using System.Collections.Generic;

namespace BlazorUtils.EasyApi.Shared.Contract;

internal class Requests
{
    private readonly IDictionary<Type, RequestAccessor> _requests;

    public IEnumerable<RequestAccessor> All => _requests.Values;

    public Requests(IDictionary<Type, RequestAccessor> requests)
    {
        _requests = requests;
    }

    public RequestAccessor<Request> Get<Request>()
        where Request : class, IRequest, new()
    {
        var requestType = typeof(Request);
        if (_requests.ContainsKey(requestType))
        {
            return (_requests[requestType] as RequestAccessor<Request>)!;
        }
        throw new SetupException($"A request of type {requestType.Name} is not registered");
    }
}
