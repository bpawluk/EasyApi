using System;
using System.Text.Json;

namespace BlazorUtils.EasyApi.Shared.Setup;

public static class SerializationSetup
{
    public static ContractBuilder WithConfiguration(this ContractBuilder builder, Action<JsonSerializerOptions> configure)
    {
        builder.JsonOptions.Configure(configure);
        return builder;
    }
}
