using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Tests.SUT.Contract.ParamTypes;
using BlazorUtils.EasyApi.Tests.SUT.Contract;

namespace BlazorUtils.EasyApi.Tests.SUT.Server.Handlers.ParamTypes;

internal class CollectionParamsRequestsHandler
    : HandlerBase
    , IHandle<ArrayParamsRequest, ArrayParamsRequest.Response>
    , IHandle<EnumerableParamsRequest, EnumerableParamsRequest.Response>
    , IHandle<ListParamsRequest, ListParamsRequest.Response>
    , IHandle<SetParamsRequest, SetParamsRequest.Response>
    , IHandle<StackParamsRequest, StackParamsRequest.Response>
    , IHandle<QueueParamsRequest, QueueParamsRequest.Response>
    , IHandle<DictionaryParamsRequest, DictionaryParamsRequest.Response>
{
    public Task<HttpResult<ArrayParamsRequest.Response>> Handle(ArrayParamsRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult<EnumerableParamsRequest.Response>> Handle(EnumerableParamsRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult<ListParamsRequest.Response>> Handle(ListParamsRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult<SetParamsRequest.Response>> Handle(SetParamsRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult<StackParamsRequest.Response>> Handle(StackParamsRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult<QueueParamsRequest.Response>> Handle(QueueParamsRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult<DictionaryParamsRequest.Response>> Handle(DictionaryParamsRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);
}
