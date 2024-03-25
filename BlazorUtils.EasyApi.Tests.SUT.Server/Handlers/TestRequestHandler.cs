using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Tests.SUT.Contract;

namespace BlazorUtils.EasyApi.Tests.SUT.Server.Handlers;

public class TestRequestHandler : IHandle<TestRequest, TestRequest.Response>
{
    public Task<TestRequest.Response> Handle(TestRequest request, CancellationToken cancellationToken)
    {
        var response = new TestRequest.Response() { Number = request.Number + 1 };
        return Task.FromResult(response);
    }
}
