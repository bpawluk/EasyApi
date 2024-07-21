using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.IntegrationTests.SUT.Contract;
using BlazorUtils.EasyApi.IntegrationTests.SUT.Server.Utils;

namespace BlazorUtils.EasyApi.IntegrationTests.SUT.Server;

internal class MethodsRequestsHandler
    : HandlerBase
    , IHandle<GetRequest>
    , IHandle<GetRequestWithResponse, MethodRequestResponse>
    , IHandle<HeadRequest>
    , IHandle<PatchRequest>
    , IHandle<PatchRequestWithResponse, MethodRequestResponse>
    , IHandle<PostRequest>
    , IHandle<PostRequestWithResponse, MethodRequestResponse>
    , IHandle<PutRequest>
    , IHandle<PutRequestWithResponse, MethodRequestResponse>
    , IHandle<DeleteRequest>
    , IHandle<DeleteRequestWithResponse, MethodRequestResponse>
{
    public Task<HttpResult> Handle(GetRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult<MethodRequestResponse>> Handle(GetRequestWithResponse request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult> Handle(HeadRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult> Handle(PatchRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult<MethodRequestResponse>> Handle(PatchRequestWithResponse request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult> Handle(PostRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult<MethodRequestResponse>> Handle(PostRequestWithResponse request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult> Handle(PutRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult<MethodRequestResponse>> Handle(PutRequestWithResponse request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult> Handle(DeleteRequest request, CancellationToken cancellationToken)
        => HandleRequest(request);

    public Task<HttpResult<MethodRequestResponse>> Handle(DeleteRequestWithResponse request, CancellationToken cancellationToken)
        => HandleRequest(request);
}
