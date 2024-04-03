using BlazorUtils.EasyApi.Tests.SUT.Server;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BlazorUtils.EasyApi.Tests.ParamTypes;

public abstract class TimeParamsTests(WebApplicationFactory<Program> factory) : TestsBase(factory)
{
}
