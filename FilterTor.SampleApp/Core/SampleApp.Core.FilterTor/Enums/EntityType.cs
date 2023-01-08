namespace SampleApp.FilterTorEx;

using FilterTor.Common.Converters;
using System.ComponentModel;
using System.Text.Json.Serialization;


[JsonConverter(typeof(JsonStringEnumCamelCaseConverter))]
public enum EntityType
{
    [Description("ترکیبی")]
    Compound = 1,

    [Description("فاکتور")]
    Invoice = 2,

    [Description("اقلام فاکتور")]
    InvoiceDetail = 3,

    [Description("مشتری")]
    Customer = 4

    //[Description("اوراق")]
    //Bond = 2,
    //[Description("سبد")]
    //Basket = 3,
    //[Description("سهام")]
    //Stock = 5,
    //[Description("پرتفو")]
    //Portfo = 6,
}
