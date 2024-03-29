using BlazorUtils.EasyApi.Tests.SUT.Contract;
using BlazorUtils.EasyApi.Tests.SUT.Server;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace BlazorUtils.EasyApi.Tests.ByParamKind;

public class ParamKindsTests(WebApplicationFactory<Program> factory) : TestsBase(factory)
{
    [Fact]
    public async Task SendingRequests_WithHeaderParams()
    {
        var request = new HeaderParamRequest() 
        {
            Number = 1,
            Text = "text",
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
            }
        };
        var caller = _sut.Services.GetRequiredService<ICall<HeaderParamRequest>>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task SendingRequests_WithRouteParams()
    {
        var request = new RouteParamRequest()
        {
            Number = 1,
            Text = "text",
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
            }
        };
        var caller = _sut.Services.GetRequiredService<ICall<RouteParamRequest>>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task SendingRequests_WithQueryStringParams()
    {
        var request = new QueryStringParamRequest()
        {
            Number = 1,
            Text = "text",
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
            }
        };
        var caller = _sut.Services.GetRequiredService<ICall<QueryStringParamRequest>>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task SendingRequests_WithBodyParams()
    {
        var request = new BodyParamRequest()
        {
            Number = 1,
            Text = "text",
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
            }
        };
        var caller = _sut.Services.GetRequiredService<ICall<BodyParamRequest>>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
