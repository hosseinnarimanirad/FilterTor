namespace SampleApp.FilterTorEx.Entities;
 
using System.ComponentModel; 


public enum CustomerMeasure
{
    [Description("مجموع خرید")]
    SumOfInvoicesPrice = 1,

    [Description("تعداد فاکتور")]
    CountOfInvoices = 2,
}
