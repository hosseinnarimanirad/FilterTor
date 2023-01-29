namespace SampleApp.FilterTorEx.Entities;

using FilterTor;
using FilterTor.Resolvers;
using FilterTor.Conditions;
using FilterTor.Expressions;
using FilterTor.Helpers;
using FilterTor.Models;
using FilterTor.Targets;
using SampleApp.Core.Entities;
using System.Linq.Expressions;
using System.Net.NetworkInformation;

public class CustomerResolver : EntityResolver<Customer>
{
    // properties
    static readonly FuncExp<Customer, decimal> _byCreditFilter = new FuncExp<Customer, decimal>(i => i.Credit);

    static readonly FuncExp<Customer, DateTime> _byRegisteredDateFilter = new FuncExp<Customer, DateTime>(i => i.RegisteredDate);

    public override Expression<Func<Customer, bool>> GetPropertyFilter(JsonTargetBase? target, string propType, Operation operation)
    {
        switch (EnumHelper.TryParseIgnoreCase<CustomerProperty>(propType))
        {
            case CustomerProperty.Credit:
                return ExpressionUtility.Compare<Customer, decimal>(_byCreditFilter.Expression, target, decimal.Parse, operation);

            case CustomerProperty.RegisteredDate:
                return ExpressionUtility.Compare<Customer, DateTime>(_byRegisteredDateFilter.Expression, target, DateTime.Parse, operation);

            default:
                throw new NotImplementedException("CustomerResolver -> GetPropertyFilter");
        }
    }

    public override Func<Customer, object> ExtractPropertyValue(string propType)
    {
        switch (EnumHelper.TryParseIgnoreCase<CustomerProperty>(propType))
        {
            case CustomerProperty.Credit:
                return i => _byCreditFilter.Func(i);

            case CustomerProperty.RegisteredDate:
                return i => _byRegisteredDateFilter.Func(i);

            default:
                throw new NotImplementedException("CustomerResolver -> ExtractPropertyValue");
        }
    }

    public override bool Validate(JsonConditionBase jsonCondition)
    {
        return Validate<CustomerProperty, CustomerCollectionProperty, CustomerMeasure>(jsonCondition);
    }

    public override bool Validate(JsonTargetBase? jsonTarget)
    {
        return Validate<CustomerProperty, CustomerCollectionProperty, CustomerMeasure>(jsonTarget);
    }
}
