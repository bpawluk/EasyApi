using BlazorUtils.EasyApi.IntegrationTests.SUT.Contract.Data;

namespace BlazorUtils.EasyApi.IntegrationTests.SUT.Contract.Response;

[Route("response-type/complex/struct")]
public class StructResponseRequest : IGet<Price> 
{
    [HeaderParam] public Price ExpectedResponse { get; init; }
}

[Route("response-type/complex/nullable-struct")]
public class NullableStructResponseRequest : IGet<Price?>
{
    [HeaderParam] public Price? ExpectedResponse { get; init; }
}

[Route("response-type/complex/class")]
public class ClassResponseRequest : IGet<Product>
{
    [HeaderParam] public Product ExpectedResponse { get; init; } = null!;
}
