namespace SampleApp.Core.FilterTor.Entities;

using System.ComponentModel;


public enum DetailInvoiceProperty
{
    [Description("شناسه فاکتور")]
    InvoiceId = 1,

    [Description("شناسه کالا")]
    ProductId = 2,

    [Description("تعداد")]
    Count = 3,

    [Description("قیمت واحد")]
    UnitPrice = 4,

    [Description("تخفیف")]
    Discount = 5,

    [Description("کالای جایزه")]
    IsPrize = 6,
}
