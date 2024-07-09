namespace BlazorUtils.EasyApi.Shared.Persistence;

internal interface IResponseStore<ResponseType>
{
    public void Save(string key, HttpResult<ResponseType> respone);

    public HttpResult<ResponseType>? Retrieve(string key);
}
