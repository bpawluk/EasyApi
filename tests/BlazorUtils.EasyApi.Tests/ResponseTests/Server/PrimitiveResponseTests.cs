﻿using Microsoft.Extensions.DependencyInjection;

namespace BlazorUtils.EasyApi.Tests.ResponseTests.Server;

public class PrimitiveResponseTests(TestsFixture fixture) : PrimitiveResponseTestsBase(fixture)
{
    protected override ICall<Request> GetCaller<Request>() => _server.Services.GetRequiredService<ICall<Request>>();

    protected override ICall<Request, Response> GetCaller<Request, Response>() => _server.Services.GetRequiredService<ICall<Request, Response>>();
}
