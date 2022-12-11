namespace SampleApp.Core.FilterTor;

using global::FilterTor.Common.Converters;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.Json.Serialization;


[JsonConverter(typeof(JsonStringEnumCamelCaseConverter))]
public enum EntityType
{
    [Description("ترکیبی")]
    Compound = 1,

    [Description("صندوق")]
    Fund = 2,

    [Description("فاکتور")]
    Invoice = 3,

    [Description("اقلام فاکتور")]
    InvoiceDetail = 4,

    //[Description("اوراق")]
    //Bond = 2,
    //[Description("سبد")]
    //Basket = 3,
    //[Description("سهام")]
    //Stock = 5,
    //[Description("پرتفو")]
    //Portfo = 6,
}
