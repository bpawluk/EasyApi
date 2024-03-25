﻿using BlazorUtils.EasyApi.Shared.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorUtils.EasyApi.Shared.Setup;

public class AppBuilder
{
    public IServiceCollection Services { get; }

    public Requests Requests { get; }

    internal AppBuilder(IServiceCollection services, Requests requests)
    {
        Services = services;
        Requests = requests;
    }
}
