﻿using Microsoft.Extensions.DependencyInjection;

namespace BlazorUtils.EasyApi.Tests.ResponseTests.Server;

public class EnumResponseTests(TestsFixture fixture) : EnumResponseTestsBase(fixture)
{
    protected override ICall<Request> GetCaller<Request>() => _server.Services.GetRequiredService<ICall<Request>>();

    protected override ICall<Request, Response> GetCaller<Request, Response>() => _server.Services.GetRequiredService<ICall<Request, Response>>();
}
