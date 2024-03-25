using BlazorUtils.EasyApi.Tests.SUT.Client;
using BlazorUtils.EasyApi.Tests.SUT.Contract;
using BlazorUtils.EasyApi.Tests.SUT.Server;
using BlazorUtils.EasyApi.Tests.Utils;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorUtils.EasyApi.Tests;

public class CallingTests : IClassFixture<WebApplicationFactory<Program>>, IDisposable
{
    private readonly App _sut;

    public CallingTests(WebApplicationFactory<Program> factory)
    {
        var httpClient = factory.CreateClient();
        var httpClientProvider = new HttpClientProvider(httpClient);
        _sut = App.Create(httpClientProvider);
    }

    [Fact]
    public async Task Test()
    {
        var request = new TestRequest() { Number = 1 };
        var caller = _sut.Services.GetRequiredService<ICall<TestRequest, TestRequest.Response>>();

        var response = await caller.Call(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(request.Number + 1, response.Number);
    }

    public void Dispose()
    {
        _sut.Dispose();
        GC.SuppressFinalize(this);
    }
}