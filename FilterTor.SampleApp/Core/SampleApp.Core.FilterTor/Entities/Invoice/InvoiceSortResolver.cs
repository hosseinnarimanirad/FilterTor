namespace SampleApp.FilterTorEx.Entities;

using FilterTor.Resolvers;
using FilterTor.Models;
using SampleApp.Core.Entities;
using System.Linq.Expressions;

public class InvoiceSortResolver : ISortResolver<Invoice>
{
    public Expression<Func<Invoice, object>> Extract(SortModel model)
    {
        switch (model.GetProperty<InvoiceProperty>())
        {
            case InvoiceProperty.InvoiceNumber:
                return e => e.InvoiceNumber;

            case InvoiceProperty.InvoiceDate:
                return e => e.InvoiceDate;

            case InvoiceProperty.IsSettled:
                return e => e.IsSettled;

            case InvoiceProperty.TotalAmount:
                return e => e.TotalAmount;
                 
            case InvoiceProperty.CustomerId:
                return e => e.CustomerId;

            case InvoiceProperty.InvoiceType:
                return e => e.InvoiceType;

            default:
                throw new NotImplementedException("InvoiceSortResolver > Extract");
        }
    }
}
