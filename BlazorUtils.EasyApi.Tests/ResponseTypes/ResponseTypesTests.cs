using BlazorUtils.EasyApi.Tests.SUT.Server;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BlazorUtils.EasyApi.Tests.ResponseTypes;

public abstract class ResponseTypesTests(WebApplicationFactory<Program> factory) : TestsBase(factory)
{
}
