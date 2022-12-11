 

//namespace GridEngine.Factory
//{
//    public static class ConditionFactory
//    {

//        #region Get Expressions

//        #endregion

//        #region DailySellSummary

//        //public static async Task<List<DailySellSummary>> GetIncludedDailySellSummariesAsync(AlborzContext context, ICondition condition)
//        //{
//        //    if (condition == null)
//        //    {
//        //        return null;
//        //    }

//        //    var whereClause = GetDailySellSummaryExpression(condition);

//        //    return await context.DailySellSummaries.Include(dss => dss.Product).AsNoTracking().Where(whereClause)?.ToListAsync();
//        //}

//        #endregion
         
//        #region Get Included Entities

//        //public static List<Customer> GetIncludedCustomers(AlborzContext context, ICondition condition)
//        //{
//        //    if (condition == null)
//        //    {
//        //        return null;
//        //    }

//        //    var whereClause = GetCustomerExpression(condition);

//        //    return context.Customers.AsNoTracking().Where(whereClause)?.ToList();
//        //}


//        #endregion

//        #region Get ConditionTypes

//        //public static List<CustomerConditionType> GetCustomerConditionTypes(ICondition condition)
//        //{
//        //    if (condition == null)
//        //    {
//        //        return new List<CustomerConditionType>();
//        //    }
//        //    if (condition is CompoundCondition compoundCondition)
//        //    {
//        //        return compoundCondition.GetCustomerConditionTypes();
//        //    }
//        //    else if (condition is CustomerCondition customerCondition)
//        //    {
//        //        return new List<CustomerConditionType>() { customerCondition.SubType };
//        //    }
//        //    else
//        //    {
//        //        return new List<CustomerConditionType>();
//        //    }
//        //}

//        #endregion

       
//    }
//}