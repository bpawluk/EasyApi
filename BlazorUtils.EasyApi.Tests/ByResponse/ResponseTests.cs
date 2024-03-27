using BlazorUtils.EasyApi.Tests.SUT.Server;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BlazorUtils.EasyApi.Tests.ByResponse;

internal class ResponseTests(WebApplicationFactory<Program> factory) : TestsBase(factory)
{
    // - NO CONTENT
    // - WITH CONTENT
    // - DIFFERENT HTTP STATUSES
    // - WITHOUT HTTP RESULT
}
