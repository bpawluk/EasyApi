namespace BlazorUtils.EasyApi.UnitTests.Utils;

public class TestResponseProvider
{
    public HttpResult<string> Response { get; set; } = HttpResult<string>.Ok("default response");
}
