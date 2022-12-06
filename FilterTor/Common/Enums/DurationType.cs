namespace FilterTor;

using FilterTor.Common.Converters;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumCamelCaseConverter))]
public enum DurationType
{
    [Description("روزانه")]
    Daily = 1,

    [Description("هفتگی")]
    Weekly = 2,

    [Description("ماهانه")]
    Monthly = 3,

    [Description("فصلی")]
    Quarterly = 4,

    [Description("سالانه")]
    Yearly = 5,

    [Description("هیچ‌کدام")]
    None = 6

}
