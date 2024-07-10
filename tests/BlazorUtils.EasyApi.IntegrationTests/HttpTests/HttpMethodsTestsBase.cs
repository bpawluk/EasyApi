using BlazorUtils.EasyApi.IntegrationTests.SUT.Contract;

namespace BlazorUtils.EasyApi.IntegrationTests.HttpTests;

public abstract class HttpMethodsTestsBase(TestsFixture fixture) : TestsBase(fixture)
{
    [Fact]
    public async Task HttpGet()
    {
        var request = new GetRequest() { Id = Guid.NewGuid() };
        await CallHttp(request);
    }

    [Fact]
    public async Task HttpGet_WithResponse()
    {
        var request = new GetRequestWithResponse() { Id = Guid.NewGuid() };
        var response = await CallHttp<GetRequestWithResponse, MethodRequestResponse>(request);
        Assert.Equal(request.Id, response.Id);
    }

    [Fact]
    public async Task HttpHead()
    {
        var request = new HeadRequest() { Id = Guid.NewGuid() };
        await CallHttp(request);
    }

    [Fact]
    public async Task HttpPatch()
    {
        var request = new PatchRequest() { Id = Guid.NewGuid() };
        await CallHttp(request);
    }

    [Fact]
    public async Task HttpPatch_WithResponse()
    {
        var request = new PatchRequestWithResponse() { Id = Guid.NewGuid() };
        var response = await CallHttp<PatchRequestWithResponse, MethodRequestResponse>(request);
        Assert.Equal(request.Id, response.Id);
    }

    [Fact]
    public async Task HttpPost()
    {
        var request = new PostRequest() { Id = Guid.NewGuid() };
        await CallHttp(request);
    }

    [Fact]
    public async Task HttpPost_WithResponse()
    {
        var request = new PostRequestWithResponse() { Id = Guid.NewGuid() };
        var response = await CallHttp<PostRequestWithResponse, MethodRequestResponse>(request);
        Assert.Equal(request.Id, response.Id);
    }

    [Fact]
    public async Task HttpPut()
    {
        var request = new PutRequest() { Id = Guid.NewGuid() };
        await CallHttp(request);
    }

    [Fact]
    public async Task HttpPut_WithResponse()
    {
        var request = new PutRequestWithResponse() { Id = Guid.NewGuid() };
        var response = await CallHttp<PutRequestWithResponse, MethodRequestResponse>(request);
        Assert.Equal(request.Id, response.Id);
    }

    [Fact]
    public async Task HttpDelete()
    {
        var request = new DeleteRequest() { Id = Guid.NewGuid() };
        await CallHttp(request);
    }

    [Fact]
    public async Task HttpDelete_WithResponse()
    {
        var request = new DeleteRequestWithResponse() { Id = Guid.NewGuid() };
        var response = await CallHttp<DeleteRequestWithResponse, MethodRequestResponse>(request);
        Assert.Equal(request.Id, response.Id);
    }
}
