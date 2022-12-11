namespace GridEngineCore.Targets;

using GridEngineCore.Common;
using GridEngineCore.Common.Converters;
using GridEngineCore.Helpers;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

[JsonConverter(typeof(TargetJsonConverter))]
public class JsonTargetBase : IJsonEntity
{
    public required TargetType TargetType { get; set; }

    public virtual string Serialize(JsonSerializerOptions? options)
    {
        if (!Validate())
            throw new NotImplementedException("JsonTargetBase > Serialize");

        return JsonSerializer.Serialize(this, GetType(), options ?? EngineCoreHelper.DefaultJsonSerializerOptions);
    }

    public virtual bool Validate()
    {
        return TargetType != 0;
    }
}