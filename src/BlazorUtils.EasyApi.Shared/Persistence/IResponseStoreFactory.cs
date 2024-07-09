namespace BlazorUtils.EasyApi.Shared.Persistence;

internal interface IResponseStoreFactory
{
    IResponseStore<ResponseType>? GetStore<ResponseType>(IRequest<ResponseType> request);
}
