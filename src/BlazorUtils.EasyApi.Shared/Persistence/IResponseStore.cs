using BlazorUtils.EasyApi.Shared.Persistence.Response;

namespace BlazorUtils.EasyApi.Shared.Persistence;

internal interface IResponseStore<ResponseType>
{
    void Save(string key, HttpResult<ResponseType> response);

    PersistedResponse<ResponseType>? Retrieve(string key);
}
