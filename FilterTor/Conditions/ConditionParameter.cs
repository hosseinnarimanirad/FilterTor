using System;
using System.Linq;

namespace FilterTor;

//public class ConditionCollectionParameter<T> : IConditionParameter
//{
//    public IQueryable<T>? Queryable { get; set; }

//    public DateTime? StartDate { get; set; }

//    public DateTime? EndDate { get; set; }

//    public DateTime? Date { get; set; }
//}

public class ConditionParameter<T> : IConditionParameter
{
    public IQueryable<T>? Queryable { get; set; }

    public T? Value { get; set; }
     
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? Date { get; set; }
}

public interface IConditionParameter
{
    DateTime? StartDate { get; set; }

    DateTime? EndDate { get; set; }

    DateTime? Date { get; set; }
}
