namespace SampleApp.FilterTorEx.Entities;

using FilterTor;
using FilterTor.Common.Entities;
using FilterTor.Conditions;
using FilterTor.Expressions;
using FilterTor.Helpers;
using FilterTor.Models;
using FilterTor.Targets;
using SampleApp.Core.Entities;
using System.Linq.Expressions;

public class InvoiceResolver : EntityResolver<Invoice>
{
    // properties
    static readonly FuncExp<Invoice, string> _byInvoiceNumberFilter = new FuncExp<Invoice, string>(i => i.InvoiceNumber);

    static readonly FuncExp<Invoice, DateTime> _byInvoiceDateFilter = new FuncExp<Invoice, DateTime>(i => i.InvoiceDate);

    static readonly FuncExp<Invoice, bool> _byIsSettledFilter = new FuncExp<Invoice, bool>(i => i.IsSettled);

    static readonly FuncExp<Invoice, decimal> _byTotalAmountFilter = new FuncExp<Invoice, decimal>(i => i.TotalAmount);

    static readonly FuncExp<Invoice, long> _byCustomerIdFilter = new FuncExp<Invoice, long>(i => i.CustomerId);

    static readonly FuncExp<Invoice, string> _byInvoiceTypeFilter = new FuncExp<Invoice, string>(i => i.InvoiceType.ToString());

    //[Description("مجموع قیمت قلم فاکتور")]
    //SumDetailInvoicesPrice = 1,
    //[Description("تعداد کالا در فاکتور")]
    //CountDetailInvoice = 2,
    //[Description("تعداد سطر در فاکتور")]
    //DistinctCountDetailInvoice = 3,
    //[Description("درصد نسبت قیمت قلم فاکتور به مبلغ کل")]
    //SumDetailInvoicesPriceToInvoicePrice = 4,
    //[Description("وجود قلم فاکتور")]
    //DetailInvoiceExists = 5,

    // measures
    //static readonly FuncExp<Invoice,decimal> _bySumDetailInvoicePrices=new FuncExp<Invoice,decimal>(i=>)

    public override Expression<Func<Invoice, bool>> GetPropertyFilter(JsonTargetBase? target, string propType, Operation operation)
    {
        switch (EnumHelper.TryParseIgnoreCase<InvoiceProperty>(propType))
        {
            case InvoiceProperty.InvoiceNumber:
                return ExpressionUtility.Compare<Invoice, string>(_byInvoiceNumberFilter.Expression, target, s => s, operation);

            case InvoiceProperty.InvoiceDate:
                return ExpressionUtility.Compare<Invoice, DateTime>(_byInvoiceDateFilter.Expression, target, DateTime.Parse, operation);

            case InvoiceProperty.IsSettled:
                return ExpressionUtility.Compare<Invoice, bool>(_byIsSettledFilter.Expression, target, bool.Parse, operation);

            case InvoiceProperty.TotalAmount:
                return ExpressionUtility.Compare<Invoice, decimal>(_byTotalAmountFilter.Expression, target, decimal.Parse, operation);

            case InvoiceProperty.CustomerId:
                return ExpressionUtility.Compare<Invoice, long>(_byCustomerIdFilter.Expression, target, long.Parse, operation);

            case InvoiceProperty.InvoiceType:
                return ExpressionUtility.Compare<Invoice, string>(_byInvoiceTypeFilter.Expression, target, s => s, operation);

            default:
                throw new NotImplementedException("InvoiceResolver -> GetPropertyFilter");
        }
    }

    public override Func<Invoice, object> ExtractPropertyValue(string propType)
    {
        switch (EnumHelper.TryParseIgnoreCase<InvoiceProperty>(propType))
        {
            case InvoiceProperty.InvoiceNumber:
                return _byInvoiceNumberFilter.Func;

            case InvoiceProperty.InvoiceDate:
                return i => _byInvoiceDateFilter.Func(i);

            case InvoiceProperty.IsSettled:
                return i => _byIsSettledFilter.Func(i);

            case InvoiceProperty.TotalAmount:
                return i => _byTotalAmountFilter.Func(i);

            case InvoiceProperty.CustomerId:
                return i => _byCustomerIdFilter.Func(i);

            case InvoiceProperty.InvoiceType:
                return _byInvoiceTypeFilter.Func;

            default:
                throw new NotImplementedException("InvoiceResolver -> ExtractPropertyValue");
        }
    }


    public override bool Validate(JsonConditionBase jsonCondition)
    {
        return Validate<InvoiceProperty, InvoiceCollectionProperty, InvoiceMeasure>(jsonCondition);
    }

    public override bool Validate(JsonTargetBase? jsonTarget)
    {
        return Validate<InvoiceProperty, InvoiceCollectionProperty, InvoiceMeasure>(jsonTarget);
    }
}
