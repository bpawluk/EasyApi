﻿using BlazorUtils.EasyApi.Tests.SUT.Server;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BlazorUtils.EasyApi.Tests.ByParamType;

internal class ComplexParamsTests(WebApplicationFactory<Program> factory) : TestsBase(factory)
{
}
