namespace FilterTor.Common.Converters;

using System.Text.Json.Serialization;
using System;
using System.Text.Json; 
using FilterTor.Conditions;
using FilterTor.Helpers;

public class ConditionJsonConverter : JsonConverter<JsonConditionBase>
{
    public override bool CanConvert(Type typeToConvert)
    {
        //return typeof(JsonConditionBase).IsAssignableFrom(typeToConvert);
        return typeof(JsonConditionBase) == typeToConvert;
    }

    public override JsonConditionBase? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return EngineCoreHelper.ParseCondition(ref reader, options);
    }

    // https://makolyte.com/system-text-json-how-to-customize-serialization-with-jsonconverter/
    // https://khalidabuhakmeh.com/serialize-interface-instances-system-text-json
    // https://getyourbitstogether.com/polymorphic-serialization-in-system-text-json/
    public override void Write(Utf8JsonWriter writer, JsonConditionBase value, JsonSerializerOptions options)
    {
        if (value is null)
        {
            writer.WriteNullValue();
            return;
        }

        writer.WriteRawValue(value.Serialize(options));
    }
}
