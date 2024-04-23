using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Tests.SUT.Contract.Data;
using BlazorUtils.EasyApi.Tests.SUT.Contract.Response;

namespace BlazorUtils.EasyApi.Tests.SUT.Server.Response;

public class ComplexResponseRequestsHandler
    : IHandle<StructResponseRequest, Price>
    , IHandle<NullableStructResponseRequest, Price?>
    , IHandle<ClassResponseRequest, Product>
{
    public Task<HttpResult<Price>> Handle(StructResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<Price>.Ok(request.ExpectedResponse));

    public Task<HttpResult<Price?>> Handle(NullableStructResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<Price?>.Ok(request.ExpectedResponse));

    public Task<HttpResult<Product>> Handle(ClassResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<Product>.Ok(request.ExpectedResponse));

}