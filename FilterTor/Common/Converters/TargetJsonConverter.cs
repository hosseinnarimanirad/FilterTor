namespace FilterTor.Common.Converters;
 
using FilterTor.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using FilterTor.Targets;

public class TargetJsonConverter : JsonConverter<JsonTargetBase>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(JsonTargetBase) == typeToConvert;
    }

    public override JsonTargetBase? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return FilterTorHelper.ParseTarget(ref reader, options);
    }

    public override void Write(Utf8JsonWriter writer, JsonTargetBase value, JsonSerializerOptions options)
    {
        if (value is null)
        {
            writer.WriteNullValue();
            return;
        }

        writer.WriteRawValue(value.Serialize(options ?? FilterTorHelper.DefaultJsonSerializerOptions));
    }
}
