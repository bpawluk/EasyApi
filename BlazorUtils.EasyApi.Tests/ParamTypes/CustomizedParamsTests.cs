using BlazorUtils.EasyApi.Tests.SUT.Server;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BlazorUtils.EasyApi.Tests.ParamTypes;

internal class CustomizedParamsTests(WebApplicationFactory<Program> factory) : TestsBase(factory)
{
}
