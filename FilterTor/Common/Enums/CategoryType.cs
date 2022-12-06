namespace FilterTor;

using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.Json.Serialization;
using FilterTor.Common.Converters;


[JsonConverter(typeof(JsonStringEnumCamelCaseConverter))]
public enum CategoryType
{
    [Description("ترکیبی")]
    Compound = 1,

    //مثل شرط روی مشتری یا کالا
    [Description("ویژگی")]
    Property = 2,

    // شرط روی یک کالکشن متعلق به یه موجودیت
    [Description("ویژگی آرایه‌ای")]
    CollectionProperty = 3,

    // شرط سنجه‌ای روی یک موجودیت
    [Description("سنجه")]
    Measure = 4,

    // شرط روی لیستی از موجودیت‌ها
    [Description("لیست")]
    List = 5,
}
