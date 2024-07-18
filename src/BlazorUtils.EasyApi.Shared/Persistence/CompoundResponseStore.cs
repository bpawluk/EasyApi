using System.Collections.Generic;

namespace BlazorUtils.EasyApi.Shared.Persistence;

internal class CompoundResponseStore<ResponseType>(IReadOnlyCollection<IResponseStore<ResponseType>> stores)
    : IResponseStore<ResponseType>
{
    private readonly IReadOnlyCollection<IResponseStore<ResponseType>> _stores = stores;

    public void Save(string key, HttpResult<ResponseType> response)
    {
        foreach (var store in _stores)
        {
            store.Save(key, response);
        }
    }

    public HttpResult<ResponseType>? Retrieve(string key)
    {
        foreach (var store in _stores)
        {
            var result = store.Retrieve(key);
            if (result is not null)
            {
                return result;
            }
        }
        return null;
    }
}
