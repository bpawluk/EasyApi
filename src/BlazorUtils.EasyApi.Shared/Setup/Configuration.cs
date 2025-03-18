using System;
using System.Text.Json;

namespace BlazorUtils.EasyApi.Shared.Setup;

public static class Configuration
{
    public static ContractBuilder ConfigureSerialization(this ContractBuilder builder, Action<JsonSerializerOptions> configure)
    {
        builder.JsonOptions.Configure(configure);
        return builder;
    }
}
