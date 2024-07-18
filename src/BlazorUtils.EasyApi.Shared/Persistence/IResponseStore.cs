namespace BlazorUtils.EasyApi.Shared.Persistence;

internal interface IResponseStore<ResponseType>
{
    void Save(string key, HttpResult<ResponseType> response);

    HttpResult<ResponseType>? Retrieve(string key);
}
