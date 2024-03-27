using BlazorUtils.EasyApi.Tests.SUT.Server;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BlazorUtils.EasyApi.Tests.ByParamKind;

internal class ParamKindsTests(WebApplicationFactory<Program> factory) : TestsBase(factory)
{
    // - BODY
    // - HEADER
    // - QUERY
    // - ROUTE
}
