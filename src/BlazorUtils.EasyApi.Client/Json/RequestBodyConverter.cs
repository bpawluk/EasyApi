using BlazorUtils.EasyApi.Shared.Contract;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorUtils.EasyApi.Client.Json;

// TODO: Configuring customized body serialization
internal class RequestBodyConverter<Request>(RequestAccessor<Request> accessor) 
    : JsonConverter<Request>
    where Request : class, IRequest, new()
{
    private readonly RequestAccessor<Request> _accessor = accessor;

    public override Request Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotSupportedException();
    }

    public override void Write(Utf8JsonWriter writer, Request request, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (var param in _accessor.BodyParams)
        {
            writer.WritePropertyName(param.Name);
            JsonSerializer.Serialize(writer, param.ReadFrom(request), options);
        }

        writer.WriteEndObject();
    }
}
