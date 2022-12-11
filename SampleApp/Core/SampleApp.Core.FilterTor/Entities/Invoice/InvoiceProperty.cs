namespace SampleApp.Core.FilterTor.Entities;

using System.ComponentModel;



public enum InvoiceProperty
{
    [Description("شماره فاکتور")]
    InvoiceNumber = 1,

    [Description("تاریخ فاکتور")]
    InvoiceDate = 2,

    [Description("تسویه شده")]
    IsSettled = 3,

    [Description("مبلغ")]
    TotalAmount = 4,

    [Description("تخفیف")]
    Discount = 5,

    [Description("شناسه مشتری")]
    CustomerId = 6,

    [Description("نوع فاکتور")]
    InvoiceType = 7,
}
 