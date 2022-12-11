using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FilterTor.Common
{
    public interface IJsonEntity
    {
        bool Validate();

        string Serialize(JsonSerializerOptions? options);
    }
}
