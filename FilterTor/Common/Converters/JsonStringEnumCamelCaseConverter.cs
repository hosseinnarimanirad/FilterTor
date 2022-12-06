﻿namespace FilterTor.Common.Converters;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public class JsonStringEnumCamelCaseConverter : JsonStringEnumConverter
{
    public JsonStringEnumCamelCaseConverter() : base(JsonNamingPolicy.CamelCase)
    {

    }
}
