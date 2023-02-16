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
using System.Collections.Generic;
using SampleApp.Core;

public class CustomerResolver : EntityResolver<Customer>
{
    // properties
    static readonly FuncExp<Customer, decimal> _byCreditFilter = new FuncExp<Customer, decimal>(i => i.Credit);

    static readonly FuncExp<Customer, DateTime> _byRegisteredDateFilter = new FuncExp<Customer, DateTime>(i => i.RegisteredDate);


    // collection properties
    static readonly FuncExp<Customer, IEnumerable<CustomerGroupType>> _byGroups =
        new FuncExp<Customer, IEnumerable<CustomerGroupType>>(c => c.CustomerGroups.Select(i => i.Type));

    // conditions that are available in the first source
    public override List<string> SecondaryConditions { get; }

    public CustomerResolver()
    {
        SecondaryConditions = new List<string>() { CustomerProperty.RegisteredDate.ToString() };
    }

    public override bool Validate(JsonConditionBase jsonCondition)
    {
        return Validate<CustomerProperty, CustomerCollectionProperty, CustomerMeasure>(jsonCondition);
    }

    public override bool Validate(JsonTargetBase? jsonTarget)
    {
        return Validate<CustomerProperty, CustomerCollectionProperty, CustomerMeasure>(jsonTarget);
    }

    public override Expression<Func<Customer, bool>> GetPropertyFilter(JsonTargetBase? target, string propType, Operation operation)
    {
        switch (EnumHelper.TryParseIgnoreCase<CustomerProperty>(propType))
        {
            case CustomerProperty.Credit:
                return ExpressionUtility.Compare(_byCreditFilter.Expression, target, decimal.Parse, operation);

            case CustomerProperty.RegisteredDate:
                return ExpressionUtility.Compare(_byRegisteredDateFilter.Expression, target, DateTime.Parse, operation);

            default:
                throw new NotImplementedException("CustomerResolver -> GetPropertyFilter");
        }
    }

    public override Func<Customer, object> ExtractPropertyValue(string propType)
    {
        List<CustomerGroup> groups = new List<CustomerGroup>();
         
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


    //public Expression<Func<Customer, bool>> GetCollectionPropertyFilter(JsonTargetBase? target, string collectionPropType, Operation operation)
    //{
    //switch (EnumHelper.TryParseIgnoreCase<CustomerCollectionProperty>(collectionPropType))
    //{
    //case CustomerCollectionProperty.Groups:
    //    return ExpressionUtility.Compare<Customer, IEnumerable<CustomerGroupType>>(
    //        _byGroups.Expression, 
    //        target, 
    //        i => Enum.Parse<CustomerGroupType>(i, true), 
    //        operation);

    //default:
    //    throw new NotImplementedException("CustomerResolver -> GetCollectionPropertyFilter");
    //}
    //}

}
