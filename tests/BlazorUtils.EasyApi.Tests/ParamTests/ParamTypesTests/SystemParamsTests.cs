using BlazorUtils.EasyApi.Tests.SUT.Contract.ParamTypes;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorUtils.EasyApi.Tests.ParamTests.ParamTypesTests;

public abstract class SystemParamsTests(TestsFixture fixture) : TestsBase(fixture)
{
    [Fact]
    public async Task Request_WithSystemParams_Guid()
    {
        var request = new GuidParamsRequest()
        {
            Guid = Guid.NewGuid(),
            GuidDefault = default,
            NullableGuidWithValue = Guid.NewGuid(),
            NullableGuidDefault = null
        };

        var result = await CallHttp<GuidParamsRequest, GuidParamsRequest.Response>(request);

        Assert.Equal(request.Guid, result.Guid);
        Assert.Equal(request.GuidDefault, result.GuidDefault);
        Assert.Equal(request.NullableGuidWithValue, result.NullableGuidWithValue);
        Assert.Equal(request.NullableGuidDefault, result.NullableGuidDefault);
    }

    [Fact]
    public async Task Request_WithSystemParams_String()
    {
        var request = new StringParamsRequest()
        {
            String = "string",
            StringEmpty = string.Empty,
            StringWhitespace = " ",
            StringDefault = null
        };

        var result = await CallHttp<StringParamsRequest, StringParamsRequest.Response>(request);

        Assert.Equal(request.String, result.String);
        Assert.Equal(request.StringEmpty, result.StringEmpty);
        Assert.Equal(request.StringWhitespace, result.StringWhitespace);
        Assert.Equal(request.StringDefault, result.StringDefault);
    }

    [Fact]
    public async Task Request_WithSystemParams_Uri()
    {
        var request = new UriParamsRequest()
        {
            HttpUri = new Uri("http://www.example.com"),
            HttpsUri = new Uri("https://www.example.com"),
            FtpUri = new Uri("ftp://ftp.example.com"),
            MailtoUri = new Uri("mailto:user@example.com"),
            FileUri = new Uri("file:///path/to/file.txt"),
            DataUri = new Uri("data:text/plain;charset=utf-8;base64,SGVsbG8sIFdvcmxkIQ=="),
            DefaultUri = null!
        };

        var result = await CallHttp<UriParamsRequest, UriParamsRequest.Response>(request);

        Assert.Equal(request.HttpUri, result.HttpUri);
        Assert.Equal(request.HttpsUri, result.HttpsUri);
        Assert.Equal(request.FtpUri, result.FtpUri);
        Assert.Equal(request.MailtoUri, result.MailtoUri);
        Assert.Equal(request.FileUri, result.FileUri);
        Assert.Equal(request.DataUri, result.DataUri);
        Assert.Equal(request.DefaultUri, result.DefaultUri);
    }
}

public class Client_SystemParamsTests(TestsFixture fixture) : SystemParamsTests(fixture)
{
    protected override ICall<Request> GetCaller<Request>() => _client.Services.GetRequiredService<ICall<Request>>();

    protected override ICall<Request, Response> GetCaller<Request, Response>() => _client.Services.GetRequiredService<ICall<Request, Response>>();
}

public class Server_SystemParamsTests(TestsFixture fixture) : SystemParamsTests(fixture)
{
    protected override ICall<Request> GetCaller<Request>() => _server.Services.GetRequiredService<ICall<Request>>();

    protected override ICall<Request, Response> GetCaller<Request, Response>() => _server.Services.GetRequiredService<ICall<Request, Response>>();
}
