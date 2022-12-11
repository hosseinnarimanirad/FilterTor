using FilterTor.Models; 
using System;
using System.Collections.Generic; 

using FilterTor.Helpers;
using SampleApp.Core.FilterTor;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class EngineHelper
{
    public static List<EnumInfo> GetAllEntities()
    {
        return EnumHelper.Parse<EntityType>();
    }

    public static List<EnumInfoWithOperations> GetProperties(EntityType type)
    {
        switch (type)
        {
            //case ConditionEntity.Customer:
            //    return EnumHelper.GetEnums<CustomerConditionType>()
            //                        //.Where(e => e != CustomerConditionType.ByCustomerPrizeGroup)
            //                        .Select(e => e.Parse(e.GetControlTypes(), e.GetAllPossibleValuesFunction())).ToList();


            case EntityType.Compound:
            default:
                throw new NotImplementedException();
        }
    }

    public static List<EnumInfo> GetMeasures(EntityType type)
    {
        switch (type)
        {
            //case ConditionEntity.Invoice:
            //    return EnumHelper.Parse<InvoiceMeasure>();

            case EntityType.Compound:
            default:
                throw new NotImplementedException();
        }
    }

    public static List<EnumInfo> GetFilters(EntityType type)
    {
        switch (type)
        {
            //case ConditionEntity.PolicyCustomerUsage:
            //    return EnumHelper.Parse<PolicyCustomerUsageFilterType>();

            case EntityType.Compound:
            default:
                throw new NotImplementedException();
        }
    }

}
