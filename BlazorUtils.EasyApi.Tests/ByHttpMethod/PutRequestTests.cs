using BlazorUtils.EasyApi.Tests.SUT.Server;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BlazorUtils.EasyApi.Tests.ByHttpMethod;

internal class PutRequestTests(WebApplicationFactory<Program> factory) : TestsBase(factory)
{
}
