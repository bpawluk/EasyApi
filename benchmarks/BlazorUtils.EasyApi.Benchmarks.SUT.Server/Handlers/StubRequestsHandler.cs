using BlazorUtils.EasyApi.Benchmarks.SUT.Contract;
using BlazorUtils.EasyApi.Benchmarks.SUT.Contract.Data;
using BlazorUtils.EasyApi.Server;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorUtils.EasyApi.Benchmarks.SUT.Server.Handlers;

public class StubRequestHandler
    : IHandle<GetRequest>
    , IHandle<GetRequestWithResponse, MethodRequestResponse<Guid>>
    , IHandle<HeadRequest>
    , IHandle<PatchRequest>
    , IHandle<PatchRequestWithResponse, MethodRequestResponse<DateOnly>>
    , IHandle<PostRequest>
    , IHandle<PostRequestWithResponse, MethodRequestResponse<DateTime>>
    , IHandle<PutRequest>
    , IHandle<PutRequestWithResponse, MethodRequestResponse<int?>>
    , IHandle<DeleteRequest>
    , IHandle<DeleteRequestWithResponse, MethodRequestResponse<Person[]>>
{
    public Task<HttpResult> Handle(GetRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult.Ok());

    public Task<HttpResult<MethodRequestResponse<Guid>>> Handle(GetRequestWithResponse request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<MethodRequestResponse<Guid>>.Ok(new() { Value = request.Value }));

    public Task<HttpResult> Handle(HeadRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult.Ok());

    public Task<HttpResult> Handle(PatchRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult.Ok());

    public Task<HttpResult<MethodRequestResponse<DateOnly>>> Handle(PatchRequestWithResponse request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<MethodRequestResponse<DateOnly>>.Ok(new() { Value = request.Value }));

    public Task<HttpResult> Handle(PostRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult.Ok());

    public Task<HttpResult<MethodRequestResponse<DateTime>>> Handle(PostRequestWithResponse request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<MethodRequestResponse<DateTime>>.Ok(new() { Value = request.Value }));

    public Task<HttpResult> Handle(PutRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult.Ok());

    public Task<HttpResult<MethodRequestResponse<int?>>> Handle(PutRequestWithResponse request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<MethodRequestResponse<int?>>.Ok(new() { Value = request.Value }));

    public Task<HttpResult> Handle(DeleteRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult.Ok());

    public Task<HttpResult<MethodRequestResponse<Person[]>>> Handle(DeleteRequestWithResponse request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<MethodRequestResponse<Person[]>>.Ok(new() { Value = request.Value }));
}
