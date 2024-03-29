﻿namespace FilterTor.Targets;

using FilterTor.Common;
using FilterTor.Common.Converters;
using FilterTor.Helpers;
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

        return JsonSerializer.Serialize(this, GetType(), options ?? FilterTorHelper.DefaultJsonSerializerOptions);
    }

    public virtual bool Validate()
    {
        return TargetType != 0;
    }

    public IEnumerable<T> GetValues<T>(Func<string, T> converter)
    {
        if (TargetType != TargetType.Array)
            throw new NotImplementedException("JsonTargetBase > GetValues");
        
        else
            return (this as JsonArrayTarget)!.Values.Select(i => converter(i));
    }
}