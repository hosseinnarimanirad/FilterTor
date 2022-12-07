namespace GridEngineCore;

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GridEngineCore.Common.Converters;


[JsonConverter(typeof(JsonStringEnumCamelCaseConverter))]
public enum TargetType
{
    [Description("مقدار")]
    Constant = 1,

    [Description("بازه")]
    Range = 2,

    [Description("آرایه")]
    Array = 3,

    [Description("ویژگی")]
    Property = 4,

    [Description("ویژگی آرایه‌ای")]
    CollectionProperty = 5,

    [Description("سنجه")]
    Measure = 6,
}
