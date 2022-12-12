namespace SampleApp.FilterTorEx.Entities;

using FilterTor.Decorators;
using FilterTor.Models;
using SampleApp.Core.Entities;
using System.ComponentModel;
using System.Linq.Expressions;

public class CustomerSortResolver : ISortResolver<Customer>
{
    public Expression<Func<Customer, object>> Extract(SortModel model)
    {
        switch (model.GetProperty<CustomerProperty>())
        {
            case CustomerProperty.Credit:
                return e => e.Credit;

            case CustomerProperty.RegisteredDate:
                return e => e.RegisteredDate;
                 
            default:
                throw new NotImplementedException("CustomerSortResolver > Extract");
        }
    }
}
