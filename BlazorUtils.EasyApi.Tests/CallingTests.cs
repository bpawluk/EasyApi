using BlazorUtils.EasyApi.Tests.SUT.Contract;
using BlazorUtils.EasyApi.Tests.SUT.Server;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorUtils.EasyApi.Tests;

public class CallingTests(WebApplicationFactory<Program> factory) : TestsBase(factory)
{
    [Fact]
    public async Task Calls()
    {
        var request = new PostRequest() { Number = 1 };
        var caller = _sut.Services.GetRequiredService<ICall<PostRequest>>();
        await caller.Call(request, CancellationToken.None);
    }

    [Fact]
    public async Task CallsWithResponse()
    {
        var request = new GetRequest() { Number = 1 };
        var caller = _sut.Services.GetRequiredService<ICall<GetRequest, GetRequest.Response>>();

        var response = await caller.Call(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(request.Number + 1, response.Number);
    }
}