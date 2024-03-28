using BlazorUtils.EasyApi.Shared.Contract;
using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorUtils.EasyApi.Client.Json;

internal class RequestBodyConverter<Request> : JsonConverter<Request>
    where Request : class, IRequest, new() 
{
    private readonly RequestAccessor<Request> _accessor;

    public RequestBodyConverter(RequestAccessor<Request> accessor)
    {
        _accessor = accessor;
    }

    public override Request Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotSupportedException();
    }

    public override void Write(Utf8JsonWriter writer, Request request, JsonSerializerOptions options)
    {
        var bodyParams = _accessor.GetBodyParams(request);
        if (bodyParams.Any())
        {
            writer.WriteStartObject();

            foreach (var param in bodyParams)
            {
                writer.WritePropertyName(param.Name);
                JsonSerializer.Serialize(writer, param.ReadFrom(request), options);
            }

            writer.WriteEndObject();
        }
    }
}
