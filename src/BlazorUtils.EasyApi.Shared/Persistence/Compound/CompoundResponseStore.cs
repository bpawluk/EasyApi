using BlazorUtils.EasyApi.Shared.Persistence.Response;
using System.Collections.Generic;

namespace BlazorUtils.EasyApi.Shared.Persistence.Compound;

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

    public PersistedResponse<ResponseType>? Retrieve(string key)
    {
        foreach (var store in _stores)
        {
            if (store.Retrieve(key) is PersistedResponse<ResponseType> response)
            {
                return response;
            }
        }
        return null;
    }
}
