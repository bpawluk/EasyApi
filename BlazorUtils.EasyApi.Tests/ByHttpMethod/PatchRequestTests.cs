using BlazorUtils.EasyApi.Tests.SUT.Server;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BlazorUtils.EasyApi.Tests.ByHttpMethod;

internal class PatchRequestTests(WebApplicationFactory<Program> factory) : TestsBase(factory)
{
}
