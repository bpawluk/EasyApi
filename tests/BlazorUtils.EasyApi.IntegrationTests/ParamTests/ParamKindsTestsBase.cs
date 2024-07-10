using BlazorUtils.EasyApi.IntegrationTests.SUT.Contract.Params;

namespace BlazorUtils.EasyApi.IntegrationTests.ParamTests;

public abstract class ParamKindsTestsBase(TestsFixture fixture) : TestsBase(fixture)
{
    [Fact]
    public async Task Request_WithoutParams()
    {
        var request = new NoParamsRequest();
        await CallHttp(request);
    }

    [Fact]
    public async Task Request_WithHeaderParams()
    {
        var request = new HeaderParamRequest()
        {
            Number = 1,
            Struct = new()
            {
                Amount = 9.99M,
                Currency = "USD"
            },
            Class = new()
            {
                Name = "T-Shirt",
                Price = 9.99,
                StockQuantity = 100,
                CreatedAt = DateTime.UtcNow
            },
            Text = "text",
            TextDefault = null,
            TextDefaultWithInitialValue = null
        };

        var result = await CallHttp<HeaderParamRequest, HeaderParamRequest.Response>(request);

        Assert.Equal(request.Number, result.Number);
        Assert.Equal(request.Struct, result.Struct);
        Assert.Equal(request.Class, result.Class);
        Assert.Equal(request.Text, result.Text);
        Assert.Equal(request.TextDefault, result.TextDefault);
        Assert.Equal(request.TextDefaultWithInitialValue, result.TextDefaultWithInitialValue);
    }

    [Fact]
    public async Task Request_WithRouteParams()
    {
        var request = new RouteParamRequest()
        {
            Number = 1,
            Struct = new()
            {
                Amount = 9.99M,
                Currency = "USD"
            },
            Class = new()
            {
                Name = "T-Shirt",
                Price = 9.99,
                StockQuantity = 100,
                CreatedAt = DateTime.UtcNow
            },
            Text = "text"
        };

        var result = await CallHttp<RouteParamRequest, RouteParamRequest.Response>(request);

        Assert.Equal(request.Number, result.Number);
        Assert.Equal(request.Struct, result.Struct);
        Assert.Equal(request.Class, result.Class);
        Assert.Equal(request.Text, result.Text);
    }

    [Fact]
    public async Task Request_WithQueryStringParams()
    {
        var request = new QueryStringParamRequest()
        {
            Number = 1,
            Struct = new()
            {
                Amount = 9.99M,
                Currency = "USD"
            },
            Class = new()
            {
                Name = "T-Shirt",
                Price = 9.99,
                StockQuantity = 100,
                CreatedAt = DateTime.UtcNow
            },
            Text = "text",
            TextDefault = null,
            TextDefaultWithInitialValue = null
        };

        var result = await CallHttp<QueryStringParamRequest, QueryStringParamRequest.Response>(request);

        Assert.Equal(request.Number, result.Number);
        Assert.Equal(request.Struct, result.Struct);
        Assert.Equal(request.Class, result.Class);
        Assert.Equal(request.Text, result.Text);
        Assert.Equal(request.TextDefault, result.TextDefault);
        Assert.Equal(request.TextDefaultWithInitialValue, result.TextDefaultWithInitialValue);
    }

    [Fact]
    public async Task Request_WithBodyParams()
    {
        var request = new BodyParamRequest()
        {
            Number = 1,
            Struct = new()
            {
                Amount = 9.99M,
                Currency = "USD"
            },
            Class = new()
            {
                Name = "T-Shirt",
                Price = 9.99,
                StockQuantity = 100,
                CreatedAt = DateTime.UtcNow
            },
            Text = "text",
            TextDefault = null,
            TextDefaultWithInitialValue = null
        };

        var result = await CallHttp<BodyParamRequest, BodyParamRequest.Response>(request);

        Assert.Equal(request.Number, result.Number);
        Assert.Equal(request.Struct, result.Struct);
        Assert.Equal(request.Class, result.Class);
        Assert.Equal(request.Text, result.Text);
        Assert.Equal(request.TextDefault, result.TextDefault);
        Assert.Equal(request.TextDefaultWithInitialValue, result.TextDefaultWithInitialValue);
    }
}
