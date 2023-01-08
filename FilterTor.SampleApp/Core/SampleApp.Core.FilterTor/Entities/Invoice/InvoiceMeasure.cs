namespace SampleApp.FilterTorEx.Entities;
 
using System.ComponentModel; 


public enum InvoiceMeasure
{
    [Description("مجموع قیمت قلم فاکتور")]
    SumDetailInvoicesPrice = 1,

    [Description("تعداد کالا در فاکتور")]
    CountDetailInvoice = 2,

    [Description("تعداد سطر در فاکتور")]
    DistinctCountDetailInvoice = 3,

    [Description("درصد نسبت قیمت قلم فاکتور به مبلغ کل")]
    SumDetailInvoicesPriceToInvoicePrice = 4,

    [Description("وجود قلم فاکتور")]
    DetailInvoiceExists = 5,
}
