using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Tests.SUT.Contract.Data;
using BlazorUtils.EasyApi.Tests.SUT.Contract.ResponseTypes;

namespace BlazorUtils.EasyApi.Tests.SUT.Server.Handlers.ResponseTypes;

internal class CollectionResponseRequestsHandler
    : HandlerBase
    , IHandle<ArrayResponseRequest, int[]>
    , IHandle<EnumerableResponseRequest, IEnumerable<Price>>
    , IHandle<ListResponseRequest, IList<Product>>
    , IHandle<SetResponseRequest, ISet<float>>
    , IHandle<StackResponseRequest, Stack<string>>
    , IHandle<QueueResponseRequest, Queue<Price>>
    , IHandle<DictionaryResponseRequest, IDictionary<string, Product>>
{
    public Task<HttpResult<int[]>> Handle(ArrayResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<int[]>.Ok(request.ExpectedResponse));

    public Task<HttpResult<IEnumerable<Price>>> Handle(EnumerableResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<IEnumerable<Price>>.Ok(request.ExpectedResponse));

    public Task<HttpResult<IList<Product>>> Handle(ListResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<IList<Product>>.Ok(request.ExpectedResponse));

    public Task<HttpResult<ISet<float>>> Handle(SetResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<ISet<float>>.Ok(request.ExpectedResponse));

    public Task<HttpResult<Stack<string>>> Handle(StackResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<Stack<string>>.Ok(request.ExpectedResponse));

    public Task<HttpResult<Queue<Price>>> Handle(QueueResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<Queue<Price>>.Ok(request.ExpectedResponse));

    public Task<HttpResult<IDictionary<string, Product>>> Handle(DictionaryResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<IDictionary<string, Product>>.Ok(request.ExpectedResponse));

}