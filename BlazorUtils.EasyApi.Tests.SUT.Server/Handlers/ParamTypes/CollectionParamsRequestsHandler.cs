using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Tests.SUT.Contract.ParamTypes;
using BlazorUtils.EasyApi.Tests.SUT.Contract;

namespace BlazorUtils.EasyApi.Tests.SUT.Server.Handlers.ParamTypes;

internal class CollectionParamsRequestsHandler
    : HandlerBase
    , IHandle<ArrayParamsRequest, ArrayParamsRequest.Response>
    , IHandle<EnumerableParamsRequest, EnumerableParamsRequest.Response>
    , IHandle<ListParamsRequest, ListParamsRequest.Response>
{
    public Task<HttpResult<ArrayParamsRequest.Response>> Handle(ArrayParamsRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult<EnumerableParamsRequest.Response>> Handle(EnumerableParamsRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult<ListParamsRequest.Response>> Handle(ListParamsRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);
}
